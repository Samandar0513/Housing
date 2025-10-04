using Housing.Models.Enums;

namespace Housing.Models.DTOs
{
    public class PropertyGetByIdDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int DistrictId { get; set; }
        public int RegionId { get; set; }

        public PropertyType PropertyType { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public CurrencyType Currency { get; set; }

        public decimal? TotalArea { get; set; }
        public int? Rooms { get; set; }
        public int? Floor { get; set; }
        public int? BuiltYear { get; set; }

        public int ViewsCount { get; set; } = 0;

        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Photos { get; set; }
    }
}
