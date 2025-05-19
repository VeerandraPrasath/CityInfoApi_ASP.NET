using Microsoft.AspNetCore.Mvc;

namespace CityInfoApi.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CityController:ControllerBase
    {
        [HttpGet()]
        public JsonResult GetCities()
        {
            return new JsonResult(new List<object>
            {
                new {id=1,name="Prasath"},
                new {id=2,name="Arun"}
            });
        }

    }
}
