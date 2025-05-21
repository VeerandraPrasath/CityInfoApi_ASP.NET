using CityInfoApi.Entity;

namespace CityInfoApi.Service
{
    public interface ICityInfoApiRepository
    {

       Task<IEnumerable<CityEntity>> GetCitiesAsync();

       Task<CityEntity> GetCityAsync(int cityId,bool includePointOfInterest);

       Task<IEnumerable<PointOfInterestEntity>> GetPointOfInterestsForCityAsync(int cityId);

      Task<PointOfInterestEntity> GetPointOfInterestForCityAsync(int cityId,int pointOfInterestId);

        Task<bool> CityExitAsync(int cityId);

    }
}
