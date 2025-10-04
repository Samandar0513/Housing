using Housing.Models;

namespace Housing.Services.Interfaces
{
    public interface IJwtTokenService
    {
        public string GenerateJwtToken(User user);
    }
}
