namespace Housing.Models
{
    public class Amenity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation
        public ICollection<PropertyAmenity> PropertyAmenities { get; set; }
    }
}
