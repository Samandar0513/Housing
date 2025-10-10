using BizLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLayer.Services.Interfaces
{
    public interface IAuthService
    {
        public AuthResult Register(RegistrDto registrDto);
        public AuthResult Login(LoginDto loginDto);
    }
}
