using Housing.Context;
using Housing.Models;
using Housing.Models.DTOs;
using Housing.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Housing.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly AppDbContext db;
        public PropertyService(AppDbContext context)
        {
            db = context;
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
                Photos = insertPropertyDto.Photos.Select(photoPath => new PropertyPhoto
                {
                    FilePath = photoPath
                }).ToList()
            };
            db.Properties.Add(property);
            db.SaveChanges();

            if (insertPropertyDto.AmenityIds != null && insertPropertyDto.AmenityIds.Any())
            {
                foreach (var amenityId in insertPropertyDto.AmenityIds)
                {
                    db.PropertyAmenities.Add(new PropertyAmenity
                    {
                        PropertyId = property.Id,
                        AmenityId = amenityId
                    });
                }
                db.SaveChanges();
            }
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
                var oldPhotos = property.Photos.ToList();
                db.PropertyPhotos.RemoveRange(oldPhotos);

                foreach (var photoPath in updatePropertyDto.Photos)
                {
                    property.Photos.Add(new PropertyPhoto
                    {
                        FilePath = photoPath,
                        CreatedAt = DateTime.UtcNow
                    });
                }
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
            db.Properties.Remove(property);
            db.SaveChanges();
            return true;
        }
    }
}
