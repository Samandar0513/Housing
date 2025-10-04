using System.Security.Cryptography;
using System.Text;

namespace Housing.Services
{
    public class Helper
    {
        public string Encript(string password, string salt)
        {
            using var algorithm = new Rfc2898DeriveBytes(
            password: password,
            salt: Encoding.UTF8.GetBytes(salt),
            iterations: 5,
            hashAlgorithm: HashAlgorithmName.SHA256);

            return Convert.ToBase64String(algorithm.GetBytes(5));
        }

        public bool Verify(string hash, string password, string salt)
        {
            var newHash = Encript(password, salt);
            return newHash == hash;
        }
    }
}
