// Housing/Controllers/PropertyFileController.cs
using BizLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Housing.Controllers
{
    [ApiController]
    [Route("api/property/files")]
    //[Authorize]
    public class PropertyFileController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IPropertyService _propertyService; 
        private const string PROPERTY_BUCKET = "property-images"; // Property rasmlar uchun bucket

        public PropertyFileController(
            IFileStorageService fileStorageService,
            IPropertyService propertyService)
        {
            _fileStorageService = fileStorageService;
            _propertyService = propertyService;
        }

        // POST /api/property/files/upload
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadPropertyImages([FromForm] List<IFormFile> files)
        {
            if (files == null || !files.Any())
                return BadRequest(new { error = "Hech qanday fayl tanlanmagan" });

            // Faqat rasm fayllarni qabul qilish
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var maxFileSize = 5 * 1024 * 1024; // 5 MB

            var uploadedUrls = new List<string>();

            foreach (var file in files)
            {
                // Validation
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest(new { error = $"Faqat rasm fayllari ({string.Join(", ", allowedExtensions)}) yuklash mumkin" });
                }

                if (file.Length > maxFileSize)
                {
                    return BadRequest(new { error = $"Fayl hajmi {maxFileSize / (1024 * 1024)} MB dan oshmasligi kerak" });
                }

                // Noyob fayl nomi yaratish
                var objectName = $"{Guid.NewGuid()}{fileExtension}";

                using (var stream = file.OpenReadStream())
                {
                    var fileUrl = await _fileStorageService.UploadFileAsync(
                        PROPERTY_BUCKET,
                        objectName,
                        stream,
                        file.ContentType
                    );

                    uploadedUrls.Add(fileUrl);
                }
            }

            return Ok(new
            {
                message = $"{uploadedUrls.Count} ta rasm muvaffaqiyatli yuklandi",
                fileUrls = uploadedUrls
            });
        }

        // DELETE /api/property/files/delete?url=...
        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePropertyImage([FromQuery] string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest(new { error = "URL kiritilishi kerak" });


            try
            {
                // URL dan objectName ni ajratib olish
                // URL: http://localhost:9000/property-images/abc123.jpg
                // objectName: abc123.jpg
                var uri = new Uri(url);
                var segments = uri.Segments;
                var objectName = segments[segments.Length - 1];

                var removed = await _fileStorageService.RemoveFileAsync(PROPERTY_BUCKET, objectName);

                if (removed)
                    return Ok(new { message = "Rasm o'chirildi" });

                return NotFound(new { error = "Rasm topilmadi" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Rasmni o'chirishda xatolik", details = ex.Message });
            }
        }
    }
}