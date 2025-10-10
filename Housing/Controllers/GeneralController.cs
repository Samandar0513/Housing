
using BizLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Housing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneralController : ControllerBase
    {
        private readonly IGeneralService generalService;
        public GeneralController(IGeneralService _generalService)
        {
            generalService = _generalService;
        }

        [HttpPost("CategoryInsert")]
        public IActionResult CategoryInsert(string categoryName)
        {
            if (categoryName == null)
            {
                return BadRequest("CategoryName bo'sh!");
            }
            var result = generalService.CategoryInsert(categoryName);
            return Ok(result);
        }
        [HttpGet("CategoryGetAll")]
        public IActionResult CategoryGetAll()
        {
            var result = generalService.CategoryGetAll();
            return Ok(result);
        }
        [HttpGet("CategoryGetById")]
        public IActionResult CategoryById(int categoryId)
        {
            var result = generalService.CategoryGetById(categoryId);
            if (result == null)
                return NotFound("Category topilmadi");
            return Ok(result);
        }
        [HttpPut("CategoryUpdate")]
        public IActionResult CategoryUpdate(int categoryId, string categoryName)
        {
            var result = generalService.CategoryUpdate(categoryId, categoryName);
            if (result == null)
                return NotFound("Category topilmadi");
            return Ok(result);
        }
        [HttpDelete("CategoryDelete")]
        public IActionResult CategoryDelete(int categoryId)
        {
            var result = generalService.CategoryDelete(categoryId);
            if (!result)
                return NotFound("Category topilmadi");
            return Ok("Category muvaffaqiyatli o'chirildi!");
        }
        //Region
        [HttpPost("RegionInsert")]
        public IActionResult RegionInsert(string regionName)
        {
            if (regionName == null)
            {
                return BadRequest("RegionName bo'sh!");
            }
            var result = generalService.RegionInsert(regionName);
            return Ok(result);
        }
        [HttpGet("RegionGetAll")]
        public IActionResult RegionGetAll()
        {
            var result = generalService.RegionGetAll();
            return Ok(result);
        }
        [HttpGet("RegionGetById")]
        public IActionResult RegionById(int regionId)
        {
            var result = generalService.RegionGetById(regionId);
            if (result == null)
                return NotFound("Region topilmadi");
            return Ok(result);
        }
        [HttpPut("RegionUpdate")]
        public IActionResult RegionUpdate(int regionId, string regionName)
        {
            var result = generalService.RegionUpdate(regionId, regionName);
            if (result == null)
                return NotFound("Region topilmadi");
            return Ok(result);
        }
        [HttpDelete("RegionDelete")]
        public IActionResult RegionDelete(int regionId)
        {
            var result = generalService.RegionDelete(regionId);
            if (!result)
                return NotFound("Region topilmadi");
            return Ok("Region muvaffaqiyatli o'chirildi!");
        }
        //District
        [HttpPost("DistrictInsert")]
        public IActionResult DistrictInsert(int regionId, string districtname)
        {
            if (districtname == null)
            {
                return BadRequest("DistrictName bo'sh!");
            }
            var result = generalService.DistrictInsert(regionId, districtname);
            return Ok(result);
        }
        [HttpGet("DistrictGetAll")]
        public IActionResult DistrictGetAll()
        {
            var result = generalService.DistrictGetAll();
            return Ok(result);
        }
        [HttpGet("DistrictGetById")]
        public IActionResult DistrictById(int districtId)
        {
            var result = generalService.DistrictGetById(districtId);
            if (result == null)
                return NotFound("District topilmadi");
            return Ok(result);
        }
        [HttpGet("DistrictGetByRegionId")]
        public IActionResult DistrictGetByRegionId(int regionId)
        {
            var result = generalService.DistrictGetByRegionId(regionId);
            if (result == null)
                return NotFound("District topilmadi");
            return Ok(result);
        }
        [HttpPut("DistrictUpdate")]
        public IActionResult DistrictUpdate(int districtId, int regionId, string districtName)
        {
            var result = generalService.DistrictUpdate(districtId, regionId, districtName);
            if (result == null)
                return NotFound("District topilmadi");
            return Ok(result);
        }
        [HttpDelete("DistrictDelete")]
        public IActionResult DistrictDelete(int districtId)
        {
            var result = generalService.DistrictDelete(districtId);
            if (!result)
                return NotFound("District topilmadi");
            return Ok("District muvaffaqiyatli o'chirildi!");
        }
        //Amenity
        [HttpPost("AmenityInsert")]
        public IActionResult AmenityInsert(string amenityName)
        {
            if (amenityName == null)
            {
                return BadRequest("AmenityName bo'sh!");
            }
            var result = generalService.AmenityInsert(amenityName);
            return Ok(result);
        }
        [HttpGet("AmenityGetAll")]
        public IActionResult AmenityGetAll()
        {
            var result = generalService.AmenityGetAll();
            return Ok(result);
        }
        [HttpGet("AmenityGetById")]
        public IActionResult AmenityById(int amenityId)
        {
            var result = generalService.AmenityGetById(amenityId);
            if (result == null)
                return NotFound("Amenity topilmadi");
            return Ok(result);
        }
        [HttpPut("AmenityUpdate")]
        public IActionResult AmenityUpdate(int amenityId, string amenityName)
        {
            var result = generalService.AmenityUpdate(amenityId, amenityName);
            if (result == null)
                return NotFound("Amenity topilmadi");
            return Ok(result);
        }
        [HttpDelete("AmenityDelete")]
        public IActionResult AmenityDelete(int amenityId)
        {
            var result = generalService.AmenityDelete(amenityId);
            if (!result)
                return NotFound("Amenity topilmadi");
            return Ok("Amenity muvaffaqiyatli o'chirildi!");
        }
    }
}
