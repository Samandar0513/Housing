using BizLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLayer.Services.Interfaces
{
    public interface IPropertyService
    {
        public int InsertProperty(int userId, PropertyInsertDto insertPropertyDto);
        public List<PropertyDto> GetAllProperties(int pageNumber, int count);
        public PropertyDto GetPropertyById(int propertyId);
        public bool DeleteProperty(int propertyId);
        public PropertyDto UpdateProperty(int propertyId, PropertyInsertDto updatePropertyDto);
        //public List<PropertyDto> SearchProperties(string query);
        //public List<PropertyDto> FilterProperties(decimal? minPrice, decimal? maxPrice, int? districtId, string? propertyType);
        //public bool IncrementViewCount(int propertyId);
        //public List<PropertyDto> GetTopViewedProperties(int count);
        //public List<PropertyDto> GetLatestProperties(int count);
        //public List<PropertyDto> GetPropertiesByCategory(int categoryId);
        //public List<PropertyDto> GetPropertiesByDistrict(int districtId);
        //public List<PropertyDto> GetPropertiesByRegion(int regionId);
        //public List<PropertyDto> GetPropertiesByPriceRange(decimal minPrice, decimal maxPrice);
        //public List<PropertyDto> GetPropertiesByRoomCount(int roomCount);
        //public List<PropertyDto> GetPropertiesByAreaRange(decimal minArea, decimal maxArea);
    }
}
