using System.ComponentModel.DataAnnotations;

namespace BizLayer.DTOs
{
    public class RegistrDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int DistrictId { get; set; }
        public string FullAddress { get; set; }
    }
}
