using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.Repositories;
using HRPortal.Entities.Dto.InComing.UserForAuth;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using Microsoft.AspNetCore.Http;
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
        private readonly DbSet<User> _dbSet;

        public AuthController(HRPortalContext context) {
            _context = context;
            _dbSet = _context.Set<User>();
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto userForRegisterDto) {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            var user = new User {
                Name = userForRegisterDto.Name,
                Surname = userForRegisterDto.Surname,
                Mail = userForRegisterDto.Email,
                CompanyId = userForRegisterDto.CompanyId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _dbSet.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto userDto) {
            var nameEntity = await _dbSet.FirstOrDefaultAsync(x => x.Mail == userDto.Email);
            if (nameEntity == null) {
                // return invalid username
                return BadRequest("invalid username");
            }

            if (!VerifyPasswordHash(userDto.Password, nameEntity.PasswordHash, nameEntity.PasswordSalt)) {
                return BadRequest("invalid pass");
            }

            return CreatedToken(nameEntity);

        }

        private string CreatedToken(User user) {

            //var claims = new[] {
            //    new Claim(ClaimTypes.Name, user.Name)
            //};

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