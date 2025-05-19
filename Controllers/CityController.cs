using Microsoft.AspNetCore.Mvc;

namespace CityInfoApi.Controllers
{
    [ApiController]
    public class CityController:ControllerBase
    {
        [HttpGet("api/cities")]
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
