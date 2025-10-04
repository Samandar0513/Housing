namespace Housing.Models
{
    public class PropertyAmenity
    {
        public int PropertyId { get; set; }
        public int AmenityId { get; set; }

        // Navigation
        public Property Property { get; set; }
        public Amenity Amenity { get; set; }
    }
}
