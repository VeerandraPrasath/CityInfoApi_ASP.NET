using CityInfoApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoApi.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CityController:ControllerBase
    {
        private CityDataSource _dataSource;
        public CityController(CityDataSource dataSource)
        {
            _dataSource = dataSource;   
        }

        [HttpGet()]
        public ActionResult<IEnumerable<CityDTO>> GetCities()
        {
           return Ok(CityDataSource.Current.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult GetCity(int id)
        {
            CityDTO city=   _dataSource.Cities.FirstOrDefault(c => c.Id == id);
            if(city is null)
            {
                return NotFound();
            }
            return Ok(city);
        }

    }
}
