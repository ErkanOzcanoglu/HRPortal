using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.Repositories;
using HRPortal.Entities.Dto.InComing.UserForAuth;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using HRPortal.Services.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HRPortal.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly HRPortalContext _context;
        private readonly DbSet<Employee> _dbSet;
        private readonly IEmailService _emailService;

        public AuthController(HRPortalContext context, IEmailService emailService) {
            _context = context;
            _dbSet = _context.Set<Employee>();
            _emailService = emailService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDto userForRegisterDto) {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            var user = new Employee {
                Name = userForRegisterDto.Name,
                Surname = userForRegisterDto.Surname,
                Mail = userForRegisterDto.Email,
                Phone = userForRegisterDto.Phone,
                TC = userForRegisterDto.TC,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()
            };

            await _dbSet.AddAsync(user);
            await _context.SaveChangesAsync();


            // return token
            return Ok(user.Id);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userDto) {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Mail == userDto.Email);
            if (user == null) {
                // return invalid username
                return BadRequest("invalid username");
            }

            if (!VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt)) {
                return BadRequest("invalid pass");
            }

            if (user.VerifiedAt == null) {
                return BadRequest("not verified");
            }

            // return user.id but not in string
            return Ok(user.Id);
        }

        [HttpPost("verifyPost")]
        public async Task<ActionResult<string>> Verify(string verificationToken) {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.VerificationToken == verificationToken);
            if (user == null) {
                // return invalid username
                return Ok("invalid token!");
            }

            user.VerifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok("User verified!");

        }

        [HttpGet("verify")]
        public async Task<ActionResult<string>> VerifyGet(string token) {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.VerificationToken == token);
            if (user == null) {
                return Ok("Invalid token");
            }

            _emailService.SendEmail(new EmailDto {
                To = user.Mail,
                Subject = "Verification",
                Body = $"Please click the link to verify your account: https://localhost:7059/api/Auth/verifyPost?token={user.VerificationToken}",                 
            });

            await _context.SaveChangesAsync();

            return Ok("user verified!");
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<Employee>> ForgotPassword(string email) {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Mail == email);
            if (user == null) { 
                return BadRequest("User not found.");
            }

            user.PasswordResetToken = CreateRandomToken();
            user.PasswordResetTokenExpiresAt = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            return Ok("You may now reset your passwords");

        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<Employee>> ChangePassword(ResetPassword request) {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.PasswordResetToken == request.Token);
            if (user == null   || user.PasswordResetTokenExpiresAt < DateTime.Now) {
                return BadRequest("Invalid token.");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiresAt = null;

            await _context.SaveChangesAsync();

            return Ok("Password successfully reset.");

        }

        private string CreatedToken(Employee user) {
            var claims = new[] {
                new Claim(ClaimTypes.Email, user.Mail)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("pcPMD09o1PSyXnrXCjTwXyr4BsezdI1AVTmud2fU4pcPMD09o1PSyXnrXCjTwXyr4BsezdI1AVTmud2fU4"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private string CreateRandomToken() {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var hmac = new HMACSHA512()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var hmac = new HMACSHA512(passwordSalt)) {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}