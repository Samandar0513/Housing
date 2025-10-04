namespace Housing.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public int? DistrictId { get; set; }
        public string FullAddress { get; set; }
        public string Role { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public District District { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
