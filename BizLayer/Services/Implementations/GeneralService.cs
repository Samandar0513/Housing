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
    public class GeneralService : IGeneralService
    {
        private readonly AppDbContext db;
        public GeneralService(AppDbContext context)
        {
            db = context;
        }
        //Category
        public int CategoryInsert(string name)
        {
            Category category = new Category()
            {
                Name = name
            };
            db.Categories.Add(category);
            db.SaveChanges();
            return category.Id;
        }
        public List<CategoryDto> CategoryGetAll()
        {
            var categories = db.Categories.ToList();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
        public CategoryDto CategoryGetById(int categoryId)
        {
            var category = db.Categories.FirstOrDefault(x => x.Id == categoryId);
            return new CategoryDto
            {
                Id = categoryId,
                Name = category.Name
            };
        }
        public CategoryDto CategoryUpdate(int categoryId, string newName)
        {
            var category = db.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null) return null;
            category.Name = newName;
            db.SaveChanges();
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        public bool CategoryDelete(int categoryId)
        {
            var category = db.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null) return false;
            db.Categories.Remove(category);
            db.SaveChanges();
            return true;
        }
        //Region
        public int RegionInsert(string name)
        {
            Region region = new Region()
            {
                Name = name
            };
            db.Regions.Add(region);
            db.SaveChanges();
            return region.Id;
        }
        public List<RegionDto> RegionGetAll()
        {
            var regions = db.Regions.ToList();
            return regions.Select(r => new RegionDto
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
        }
        public RegionDto RegionGetById(int regionId)
        {
            var region = db.Regions.FirstOrDefault(x => x.Id == regionId);
            return new RegionDto
            {
                Id = regionId,
                Name = region.Name
            };
        }
        public RegionDto RegionUpdate(int regionId, string newName)
        {
            var region = db.Regions.FirstOrDefault(r => r.Id == regionId);
            if (region == null) return null;
            region.Name = newName;
            db.SaveChanges();
            return new RegionDto
            {
                Id = region.Id,
                Name = region.Name
            };
        }
        public bool RegionDelete(int regionId)
        {
            var region = db.Regions.FirstOrDefault(r => r.Id == regionId);
            if (region == null) return false;
            db.Regions.Remove(region);
            db.SaveChanges();
            return true;
        }
        //District
        public int DistrictInsert(int regionId, string name)
        {
            District district = new District()
            {
                RegionId = regionId,
                Name = name
            };
            db.Districts.Add(district);
            db.SaveChanges();
            return district.Id;
        }
        public List<DistrictDto> DistrictGetAll()
        {
            var districts = db.Districts
                .Include(d => d.Region)
                .ToList();
            return districts.Select(d => new DistrictDto
            {
                Id = d.Id,
                Name = d.Name,
                RegionId = d.RegionId,
                RegionName = d.Region.Name
            }).ToList();
        }
        public DistrictDto DistrictGetById(int districtId)
        {
            var district = db.Districts
                .Include(d => d.Region)
                .FirstOrDefault(x => x.Id == districtId);
            return new DistrictDto
            {
                Id = districtId,
                Name = district.Name,
                RegionId = district.RegionId,
                RegionName = district.Region.Name
            };
        }
        public List<DistrictDto> DistrictGetByRegionId(int regionId)
        {
            var districts = db.Districts
                .Include(d => d.Region)
                .Where(d => d.RegionId == regionId)
                .ToList();
            return districts.Select(d => new DistrictDto
            {
                Id = d.Id,
                Name = d.Name,
                RegionId = d.RegionId,
                RegionName = d.Region.Name
            }).ToList();
        }
        public DistrictDto DistrictUpdate(int districtId, int regionId, string newName)
        {
            var district = db.Districts.FirstOrDefault(d => d.Id == districtId);
            if (district == null) return null;
            district.Name = newName;
            district.RegionId = regionId;
            db.SaveChanges();
            return new DistrictDto
            {
                Id = district.Id,
                Name = district.Name,
                RegionId = district.RegionId,
                RegionName = db.Regions.FirstOrDefault(r => r.Id == district.RegionId).Name
            };
        }
        public bool DistrictDelete(int districtId)
        {
            var district = db.Districts.FirstOrDefault(d => d.Id == districtId);
            if (district == null) return false;
            db.Districts.Remove(district);
            db.SaveChanges();
            return true;
        }
        //Amenity
        public int AmenityInsert(string name)
        {
            Amenity amenity = new Amenity()
            {
                Name = name
            };
            db.Amenities.Add(amenity);
            db.SaveChanges();
            return amenity.Id;
        }
        public List<AmenityDto> AmenityGetAll()
        {
            var amenities = db.Amenities.ToList();
            return amenities.Select(a => new AmenityDto
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();
        }
        public AmenityDto AmenityGetById(int amenityId)
        {
            var amenity = db.Amenities.FirstOrDefault(x => x.Id == amenityId);
            return new AmenityDto
            {
                Id = amenityId,
                Name = amenity.Name
            };
        }
        public AmenityDto AmenityUpdate(int amenityId, string newName)
        {
            var amenity = db.Amenities.FirstOrDefault(a => a.Id == amenityId);
            if (amenity == null) return null;
            amenity.Name = newName;
            db.SaveChanges();
            return new AmenityDto
            {
                Id = amenity.Id,
                Name = amenity.Name
            };
        }
        public bool AmenityDelete(int amenityId)
        {
            var amenity = db.Amenities.FirstOrDefault(a => a.Id == amenityId);
            if (amenity == null) return false;
            db.Amenities.Remove(amenity);
            db.SaveChanges();
            return true;
        }
    }
}
