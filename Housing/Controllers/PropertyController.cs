using BizLayer.DTOs;
using BizLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Housing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService propertyService;
        public PropertyController(IPropertyService _propertyService)
        {
            propertyService = _propertyService;
        }

        [HttpPost("PropertyInsert")]
        public IActionResult CreateProperty(PropertyInsertDto propertyInsertDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Foydalanuvchi topilmadi");
            var result = propertyService.InsertProperty(int.Parse(userId), propertyInsertDto);
            return Ok(result);
        }
        [HttpGet("PropertyGetById")]
        [AllowAnonymous]
        public IActionResult PropertyGetById(int propertyId)
        {
            var result = propertyService.GetPropertyById(propertyId);
            if (result == null)
                return NotFound("Uy topilmadi");
            return Ok(result);
        }
        [HttpGet("PropertyGetAll")]
        [AllowAnonymous]
        public IActionResult PropertyGetAll(int pageNumber = 1, int count = 10)
        {
            var result = propertyService.GetAllProperties(pageNumber, count);
            return Ok(result);
        }
        [HttpDelete("PropertyDelete")]
        public IActionResult PropertyDelete(int propertyId)
        {
            var result = propertyService.DeleteProperty(propertyId);
            if (!result)
                return NotFound("Uy topilmadi");
            return Ok("E'lon muvaffaqiyatli o'chirildi!");
        }
        [HttpPut("PropertyUpdate")]
        public IActionResult PropertyUpdate(int propertyId, PropertyInsertDto updatePropertyDto)
        {
            var result = propertyService.UpdateProperty(propertyId, updatePropertyDto);
            return Ok(result);
        }
    }
}
