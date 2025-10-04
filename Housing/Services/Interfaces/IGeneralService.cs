using Housing.Models.DTOs;

namespace Housing.Services.Interfaces
{
    public interface IGeneralService
    {
        //Category
        public int CategoryInsert(string name);
        public List<CategoryDto> CategoryGetAll();
        public CategoryDto CategoryGetById(int categoryId);
        public CategoryDto CategoryUpdate(int categoryId, string newName);
        public bool CategoryDelete(int categoryId);
        //Region
        public int RegionInsert(string name);
        public List<RegionDto> RegionGetAll();
        public RegionDto RegionGetById(int regionId);
        public RegionDto RegionUpdate(int regionId, string newName);
        public bool RegionDelete(int regionId);
        //District
        public int DistrictInsert(int regionId, string name);
        public List<DistrictDto> DistrictGetAll();
        public DistrictDto DistrictGetById(int districtId);
        public DistrictDto DistrictUpdate(int districtId, int regionId, string newName);
        public bool DistrictDelete(int districtId);
        public List<DistrictDto> DistrictGetByRegionId(int regionId);
        //Amenity
        public int AmenityInsert(string name);
        public List<AmenityDto> AmenityGetAll();
        public AmenityDto AmenityGetById(int amenityId);
        public AmenityDto AmenityUpdate(int amenityId, string newName);
        public bool AmenityDelete(int amenityId);
    }
}
