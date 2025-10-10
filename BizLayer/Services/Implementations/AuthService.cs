using BizLayer.DTOs;
using BizLayer.Services.Interfaces;
using DataAccess.Context;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLayer.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext db;
        private readonly IJwtTokenService _tokenService;
        private readonly Helper _helper;
        public AuthService(AppDbContext context, IJwtTokenService tokenService, Helper helper)
        {
            db = context;
            _tokenService = tokenService;
            _helper = helper;
        }
        public AuthResult Register(RegistrDto registrDto)
        {
            if (db.Users.Any(u => u.Email == registrDto.Email))
            {
                return new AuthResult
                {
                    IsSuccess = false,
                    Message = "Ushbu email allaqachon mavjud!",
                    StatusCode = 409
                };
            }
            if (db.Users.Any(x => x.Phone == registrDto.Phone))
            {
                return new AuthResult
                {
                    IsSuccess = false,
                    Message = """
                        Telefon raqami allaqachon mavjud!
                        Boshqa raqam kiriting!
                    """,
                    StatusCode = 409
                };
            }

            var salt = Guid.NewGuid().ToString();
            var hashPassword = _helper.Encript(registrDto.Password, salt);

            var user = new User
            {
                Name = registrDto.Name,
                Email = registrDto.Email,
                Phone = registrDto.Phone,
                Password = hashPassword,
                Salt = salt,
                DistrictId = registrDto.DistrictId,
                FullAddress = registrDto.FullAddress,
                Role = "User"
            };
            db.Users.Add(user);
            db.SaveChanges();

            var token = _tokenService.GenerateJwtToken(user);
            return new AuthResult
            {
                IsSuccess = true,
                Message = "Ro'yxatdan o'tish muvaffaqiyatli",
                Token = token,
                UserDto = new UserDto
                {
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    Role = user.Role
                },
                StatusCode = 201
            };
        }

        public AuthResult Login(LoginDto loginDto)
        {
            var user = db.Users.FirstOrDefault(x => x.Email == loginDto.Email);

            if (!Tekshir(loginDto, user))
            {
                return new AuthResult
                {
                    IsSuccess = false,
                    Message = "Email yoki parol xato!",
                    StatusCode = 401
                };
            }

            var token = _tokenService.GenerateJwtToken(user);
            return new AuthResult
            {
                IsSuccess = true,
                Message = "Kirish muvaffaqiyatli!",
                Token = token,
                UserDto = new UserDto
                {
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    Role = user.Role
                },
                StatusCode = 200
            };
        }
        private bool Tekshir(LoginDto loginDto, User user)
        {
            if (user == null) return false;
            return _helper.Verify(user.Password, loginDto.Password, user.Salt);
        }
    }
}
