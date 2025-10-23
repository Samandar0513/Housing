using BizLayer.DTOs;
using BizLayer.Services.Interfaces;
using DataAccess.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLayer.Services.Implementations
{
    public class PropertyService : IPropertyService
    {
        private readonly AppDbContext db;
        private readonly IFileStorageService _fileStorage;
        private const string PROPERTY_BUCKET = "property-images";
        public PropertyService(AppDbContext context, IFileStorageService fileStorage)
        {
            db = context;
            _fileStorage = fileStorage;
        }
        public int InsertProperty(int userId, PropertyInsertDto insertPropertyDto)
        {
            Property property = new Property()
            {
                UserId = userId,
                CategoryId = insertPropertyDto.CategoryId,
                DistrictId = insertPropertyDto.DistrictId,
                RegionId = insertPropertyDto.RegionId,
                PropertyType = insertPropertyDto.PropertyType,
                Description = insertPropertyDto.Description,
                Price = insertPropertyDto.Price,
                Currency = insertPropertyDto.Currency,
                TotalArea = insertPropertyDto.TotalArea,
                Rooms = insertPropertyDto.Rooms,
                Floor = insertPropertyDto.Floor,
                BuiltYear = insertPropertyDto.BuiltYear,
                ContactName = insertPropertyDto.ContactName,
                ContactPhone = insertPropertyDto.ContactPhone,
                ViewsCount = 0,
                CreatedAt = DateTime.UtcNow,
                //Photos = insertPropertyDto.Photos.Select(photoPath => new PropertyPhoto
                //{
                //    FilePath = photoPath
                //}).ToList()

                //// ✅ Navigation property'lar orqali qo'shish
                //Photos = insertPropertyDto.Photos?.Select(photoUrl => new PropertyPhoto
                //{
                //    FilePath = photoUrl
                //}).ToList() ?? new List<PropertyPhoto>(),

                //PropertyAmenities = insertPropertyDto.AmenityIds?.Select(amenityId => new PropertyAmenity
                //{
                //    AmenityId = amenityId
                //}).ToList() ?? new List<PropertyAmenity>()
            };
            db.Properties.Add(property);
            db.SaveChanges();
            // 2. Photos qo'shish
            if (insertPropertyDto.Photos != null && insertPropertyDto.Photos.Any())
            {
                var photos = insertPropertyDto.Photos.Select(photoUrl => new PropertyPhoto
                {
                    PropertyId = property.Id,
                    FilePath = photoUrl,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

                db.PropertyPhotos.AddRange(photos); // ✅ AddRange - tezroq
            }

            // 3. Amenities qo'shish
            if (insertPropertyDto.AmenityIds != null && insertPropertyDto.AmenityIds.Any())
            {
                var amenities = insertPropertyDto.AmenityIds.Select(amenityId => new PropertyAmenity
                {
                    PropertyId = property.Id,
                    AmenityId = amenityId
                }).ToList();

                db.PropertyAmenities.AddRange(amenities); // ✅ AddRange - tezroq
            }

            db.SaveChanges(); // ✅ Faqat bir marta save
            return property.Id;
        }
        public List<PropertyDto> GetAllProperties(int pageNumber, int count)
        {
            var properties = db.Properties
            .Include(p => p.District)
                .ThenInclude(d => d.Region)
            .Include(p => p.Category)
            .Include(p => p.Photos)
            .Include(p => p.PropertyAmenities)
                .ThenInclude(pa => pa.Amenity)
            .OrderByDescending(p => p.CreatedAt)
            .Skip((pageNumber - 1) * count)
            .Take(count)
            .ToList();

            return properties.Select(p => new PropertyDto
            {
                Id = p.Id,
                UserId = p.UserId,
                CategoryName = p.Category.Name,
                DistrictName = p.District.Name,
                RegionName = p.District.Region.Name,
                PropertyType = p.PropertyType.ToString(),
                Description = p.Description,
                Price = p.Price,
                Currency = p.Currency.ToString(),
                TotalArea = p.TotalArea,
                Rooms = p.Rooms,
                Floor = p.Floor,
                BuiltYear = p.BuiltYear,
                ContactName = p.ContactName,
                ContactPhone = p.ContactPhone,
                ViewsCount = p.ViewsCount,
                CreatedAt = p.CreatedAt,
                Photos = p.Photos.Select(x => x.FilePath).ToList(),
                Amenities = p.PropertyAmenities.Select(pa => pa.Amenity.Name).ToList()
            }).ToList();
        }
        public PropertyDto GetPropertyById(int propertyId)
        {
            var property = db.Properties
            .Include(p => p.District)
            .ThenInclude(d => d.Region)
            .Include(p => p.Category)
            .Include(p => p.Photos)
            .Include(p => p.PropertyAmenities)
            .ThenInclude(pa => pa.Amenity)
            .FirstOrDefault(p => p.Id == propertyId);

            if (property == null) return null;
            property.ViewsCount += 1;
            db.SaveChanges();
            return new PropertyDto
            {
                Id = property.Id,
                UserId = property.UserId,
                CategoryName = property.Category.Name,
                DistrictName = property.District.Name,
                RegionName = property.Region.Name,
                PropertyType = property.PropertyType.ToString(),
                Description = property.Description,
                Price = property.Price,
                Currency = property.Currency.ToString(),
                TotalArea = property.TotalArea,
                Rooms = property.Rooms,
                Floor = property.Floor,
                BuiltYear = property.BuiltYear,
                ContactName = property.ContactName,
                ContactPhone = property.ContactPhone,
                ViewsCount = property.ViewsCount,
                CreatedAt = property.CreatedAt,
                Photos = property.Photos.Select(x => x.FilePath).ToList(),
                Amenities = property.PropertyAmenities.Select(pa => pa.Amenity.Name).ToList()
            };
        }
        public PropertyDto UpdateProperty(int propertyId, PropertyInsertDto updatePropertyDto)
        {
            var property = db.Properties
                .Include(p => p.Photos)
                .Include(p => p.PropertyAmenities)
                .FirstOrDefault(p => p.Id == propertyId);

            if (property == null)
                return null;

            property.CategoryId = updatePropertyDto.CategoryId;
            property.DistrictId = updatePropertyDto.DistrictId;
            property.PropertyType = updatePropertyDto.PropertyType;
            property.Description = updatePropertyDto.Description;
            property.Price = updatePropertyDto.Price;
            property.Currency = updatePropertyDto.Currency;
            property.TotalArea = updatePropertyDto.TotalArea;
            property.Rooms = updatePropertyDto.Rooms;
            property.Floor = updatePropertyDto.Floor;
            property.BuiltYear = updatePropertyDto.BuiltYear;
            property.ContactName = updatePropertyDto.ContactName;
            property.ContactPhone = updatePropertyDto.ContactPhone;

            if (updatePropertyDto.Photos != null)
            {
                //var oldPhotos = property.Photos.ToList();
                //db.PropertyPhotos.RemoveRange(oldPhotos);

                //foreach (var photoPath in updatePropertyDto.Photos)
                //{
                //    property.Photos.Add(new PropertyPhoto
                //    {
                //        FilePath = photoPath,
                //        CreatedAt = DateTime.UtcNow
                //    });
                //}
                var oldPhotos = db.PropertyPhotos
                    .Where(p => p.PropertyId == propertyId)
                    .ToList();
                var newPhotos = updatePropertyDto.Photos
                    .Select(p => new PropertyPhoto
                    {
                        FilePath = p,
                        CreatedAt = DateTime.UtcNow,
                        PropertyId = propertyId
                    }).ToList();
                var photosToRemove = oldPhotos
                    .Where(op => !newPhotos.Any(np => np.FilePath == op.FilePath))
                    .ToList();
                var photosToAdd = newPhotos
                    .Where(np => !oldPhotos.Any(op => op.FilePath == np.FilePath))
                    .ToList();

                db.PropertyPhotos.RemoveRange(photosToRemove);
                db.PropertyPhotos.AddRange(photosToAdd);
                db.SaveChanges();
            }

            if (updatePropertyDto.AmenityIds != null)
            {
                var oldAmenities = property.PropertyAmenities.ToList();
                db.PropertyAmenities.RemoveRange(oldAmenities);

                foreach (var amenityId in updatePropertyDto.AmenityIds)
                {
                    property.PropertyAmenities.Add(new PropertyAmenity
                    {
                        AmenityId = amenityId
                    });
                }
            }

            db.SaveChanges();

            return GetPropertyById(propertyId);
        }
        public bool DeleteProperty(int propertyId)
        {
            var property = db.Properties.FirstOrDefault(p => p.Id == propertyId);
            if (property == null) return false;
            // ✅ Rasmlarni Minio'dan o'chirish
            foreach (var photo in property.Photos)
            {
                try
                {
                    var uri = new Uri(photo.FilePath);
                    var objectName = uri.Segments[uri.Segments.Length - 1];
                    _fileStorage.RemoveFileAsync(PROPERTY_BUCKET, objectName).Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Rasmni o'chirishda xatolik: {ex.Message}");
                }
            }

            db.Properties.Remove(property);
            db.SaveChanges();
            return true;
        }
    }
}
