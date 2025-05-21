using CityInfoApi.DbContexts;
using CityInfoApi.Entity;
using CityInfoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfoApi.Service
{
    public class CityInfoApiRepository : ICityInfoApiRepository
    {
        private CityInfoApiContext _context;
        public CityInfoApiRepository(CityInfoApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CityExitAsync(int cityId)
        {
           return await _context.Cities.AnyAsync(city => city.Id == cityId);
        }

        public async Task<IEnumerable<CityEntity>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<CityEntity> GetCityAsync(int cityId, bool includePointOfInterest)
        {
            if(includePointOfInterest)
            {
                CityEntity city= await _context.Cities.Include(c => c.PointOfInterests).Where(city => city.Id == cityId).FirstOrDefaultAsync();
                return city;
            }

            return await _context.Cities.Where(city => city.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterestEntity> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointOfInterests.Where(poi => poi.CityId == cityId && poi.Id == pointOfInterestId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterestEntity>> GetPointOfInterestsForCityAsync(int cityId)
        {
            return await _context.PointOfInterests.Where(poi => poi.CityId == cityId).ToListAsync();
        }
    }
}
