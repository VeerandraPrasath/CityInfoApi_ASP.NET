using CityInfoApi.DbContexts;
using CityInfoApi.Entity;
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
        public async Task<IEnumerable<CityEntity>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<CityEntity> GetCityAsync(int cityId)
        {
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
