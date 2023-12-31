﻿using HRPortal.DataAccessLayer.Context;
using HRPortal.Entities.Dto.InComing.UserForAuth;
using HRPortal.Entities.Dto.OutComing;
using HRPortal.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.DataAccessLayer.Repositories {
    public class AuthRepository {
        private readonly HRPortalContext _context;
        private readonly DbSet<User> _dbSet;

        public AuthRepository(HRPortalContext context) {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public async Task Register(RegisterDto userForRegisterDto) {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            var user = new User {
                Name = userForRegisterDto.Name,
                Surname = userForRegisterDto.Surname,
                Mail = userForRegisterDto.Email,
                Phone = userForRegisterDto.Phone,
                TC = userForRegisterDto.TC,
                Title = userForRegisterDto.Title,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CompanyId = userForRegisterDto.CompanyGuid,
                VerificationToken = CreateRandomToken()
            };

            await _dbSet.AddAsync(user);
        }

        public async Task<ActionResult<string>> Login(AuthDto userDto) {
            var nameEntity = await _dbSet.FirstOrDefaultAsync(x => x.Mail == userDto.Email);
            if (nameEntity == null) {
                return new UnauthorizedResult();
            }

            if (!VerifyPasswordHash(userDto.Password, nameEntity.PasswordHash, nameEntity.PasswordSalt)) {
                return new UnauthorizedResult();
            }

            return CreatedToken(nameEntity);

        }

        private string CreateRandomToken() {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        private string CreatedToken(User user) {

            //var claims = new[] {
            //    new Claim(ClaimTypes.Name, user.Name)
            //};

            var claims = new[] {
                new Claim(ClaimTypes.Email, user.Mail)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super secret key"));

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