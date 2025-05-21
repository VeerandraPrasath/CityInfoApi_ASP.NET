using CityInfoApi.Entity;

namespace CityInfoApi.Service
{
    public interface ICityInfoApiRepository
    {

       Task<IEnumerable<CityEntity>> GetCitiesAsync();

       Task<CityEntity> GetCityAsync(int cityId);

       Task<IEnumerable<PointOfInterestEntity>> GetPointOfInterestsForCityAsync(int cityId);

      Task<PointOfInterestEntity> GetPointOfInterestForCityAsync(int cityId,int pointOfInterestId);
    }
}
