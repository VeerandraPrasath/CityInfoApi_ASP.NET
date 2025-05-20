using CityInfoApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoApi.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointOfInterestController:ControllerBase
    {
        [HttpGet()]
        public ActionResult<IEnumerable<PointOfInterestDTO>> GetPointOfInterests(int cityId)
        {
            CityDTO city = CityDataSource.Current.Cities.Where(city => city.Id == cityId).FirstOrDefault();
            if(city is null)
            {
                return NotFound(); //203 status code
            }
            if(city.PointOfInterestDTOs.Count == 0)
            {
                return NotFound();
            }
            return Ok(city.PointOfInterestDTOs);

        }

        [HttpGet("{poiId}")]
        public ActionResult<PointOfInterestDTO> GetPointOfInterest(int cityId, int poiId)
        {
            CityDTO city = CityDataSource.Current.Cities.Where(city => city.Id == cityId).FirstOrDefault();
            if (city is null)
            {
                return NotFound(); //203 status code
            }
            if (city.PointOfInterestDTOs.Count == 0)
            {
                return NotFound();
            }
            PointOfInterestDTO pointOfInterest = city.PointOfInterestDTOs.Where(poi => poi.Id == poiId).FirstOrDefault();
            if (pointOfInterest is null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }
             
    }
}
