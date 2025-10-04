using Housing.Models.DTOs;

namespace Housing.Services.Interfaces
{
    public interface IAuthService
    {
        public AuthResult Register(RegistrDto registrDto);
        public AuthResult Login(LoginDto loginDto);
    }
}
