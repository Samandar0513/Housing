using Housing.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Housing.Models.DTOs
{
    public class PropertyInsertDto
    {
        public int CategoryId { get; set; }
        public int DistrictId { get; set; }
        public int RegionId { get; set; }
        [EnumDataType(typeof(PropertyType))]
        public PropertyType PropertyType { get; set; } = PropertyType.Sale;
        public string Description { get; set; }
        public decimal Price { get; set; }
        [EnumDataType(typeof(CurrencyType))]
        public CurrencyType Currency { get; set; } = CurrencyType.EUR;
        public decimal? TotalArea { get; set; }
        public int? Rooms { get; set; }
        public int? Floor { get; set; }
        public int? BuiltYear { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public List<string> Photos { get; set; }
    }
}
