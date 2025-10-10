using Domain.Enums;

namespace Domain.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int DistrictId { get; set; }
        public int RegionId { get; set; }
        public PropertyType PropertyType { get; set; } = PropertyType.Sale;
        public string Description { get; set; }
        public decimal Price { get; set; }
        public CurrencyType Currency { get; set; } = CurrencyType.EUR;
        public decimal? TotalArea { get; set; }
        public int? Rooms { get; set; }
        public int? Floor { get; set; }
        public int? BuiltYear { get; set; }
        public int ViewsCount { get; set; } = 0;

        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public User User { get; set; }
        public Category Category { get; set; }
        public District District { get; set; }
        public Region Region { get; set; }
        public ICollection<PropertyDocument> Documents { get; set; }
        public ICollection<PropertyAmenity> PropertyAmenities { get; set; }
        public ICollection<PropertyPhoto> Photos { get; set; }
    }
}
