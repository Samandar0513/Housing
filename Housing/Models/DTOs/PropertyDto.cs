﻿namespace Housing.Models.DTOs
{
    public class PropertyDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public string DistrictName { get; set; }
        public string RegionName { get; set; }

        public string PropertyType { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }

        public decimal? TotalArea { get; set; }
        public int? Rooms { get; set; }
        public int? Floor { get; set; }
        public int? BuiltYear { get; set; }

        public int ViewsCount { get; set; } = 0;

        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Photos { get; set; }
        public List<string> Amenities { get; set; }
    }
}
