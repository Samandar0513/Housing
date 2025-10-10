namespace Domain.Entities
{
    public class District
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public string Name { get; set; }

        // Navigation
        public Region Region { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
