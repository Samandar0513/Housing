using System.ComponentModel.DataAnnotations;

namespace BizLayer.DTOs
{
    public class RegistrDto
    {
        [Required]
        public string Name { get; set; }
        [Required, Phone]
        public string Phone { get; set; }
        [Required, EmailAddress(ErrorMessage = "Email formati noto'g'ri!")]
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
        public int DistrictId { get; set; }
        public string FullAddress { get; set; }
    }
}
