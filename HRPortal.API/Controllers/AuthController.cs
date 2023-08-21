using HRPortal.DataAccessLayer.Context;
using HRPortal.DataAccessLayer.Repositories;
using HRPortal.Entities.Dto.InComing.DtoForUserPersonalInfo;
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
        

        public AuthController(HRPortalContext context) {
            _context = context;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserPersonalInfo>> Register(UserForRegisterDto userForRegisterDto) {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            var user = new UserPersonalInfo {
                FirstName = userForRegisterDto.Name,
                LastName = userForRegisterDto.Surname,
                EmailAddress = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _context.UserPersonalInfo.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserAuthDto userDto) {
            var entity = await _context.UserPersonalInfo.FirstOrDefaultAsync(x => x.EmailAddress == userDto.Email);
            if (entity == null) {
                // return invalid username
                return BadRequest("invalid username");
            }

            if (!VerifyPasswordHash(userDto.Password, entity.PasswordHash, entity.PasswordSalt)) {
                return BadRequest("invalid pass");
            }

            return CreatedToken(entity);

        }

        private string CreatedToken(UserPersonalInfo user) {

            //var claims = new[] {
            //    new Claim(ClaimTypes.Name, user.Name)
            //};

            var claims = new[] {
                new Claim(ClaimTypes.Email, user.EmailAddress)
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