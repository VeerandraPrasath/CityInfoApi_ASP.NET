using CityInfoApi.MailService;
using CityInfoApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoApi.Controllers
{
    [ApiController] //When we remove this,It creating textBox for properties while posting in SwaggerUI.WHy ?
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointOfInterestController:ControllerBase
    {
        //ILogger<PointOfInterestController> _logger;
        private CityDataSource _dataSource;
        private IMailService _mailservice;

        public PointOfInterestController(CityDataSource dataSource,IMailService mailService)
        {
            _dataSource = dataSource;
            _mailservice = mailService;
            //_logger = logger;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<PointOfInterestDTO>> GetPointOfInterests(int cityId)
        {
            try
            {
                //This thorow is for catch the the exception and log the critical info in the console.
                //throw new Exception("Test Exception for logging critical infos");

                CityDTO city = _dataSource.Cities.Where(city => city.Id == cityId).FirstOrDefault();
                if (city is null)
                {
                    //_logger.LogInformation($"City with id {cityId} was not found when accessing point of interest");-->commented because we are using serilogger.
                    return NotFound(); //203 status code
                }
                if (city.PointOfInterestDTOs.Count == 0)
                {
                    return NotFound();
                }
                return Ok(city.PointOfInterestDTOs);
            }
            catch (Exception ex)
            {
                //_logger.LogCritical($"Exception while getting point of interest for city with id {cityId}.", ex); //We only mention to log informaton in the Launchsettings.json but the critical things are also logging in the console.
                return StatusCode(500, "A problem happened while handling your request.");

            }

        }

        [HttpGet("{poiId}",Name ="GetPointOfInterest")]
        public ActionResult<PointOfInterestDTO> GetPointOfInterest(int cityId, int poiId)
        {
            CityDTO city = _dataSource.Cities.Where(city => city.Id == cityId).FirstOrDefault();
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

        [HttpPost()]
        public ActionResult CreatePointOfInterest(int cityId, CreationPointOfInterestDTO creationPointOfInterestDTO)
        {
            CityDTO city = _dataSource.Cities.Where(city => city.Id == cityId).FirstOrDefault();
            if(city is null)
            {
                return NotFound(); //203 status code
            }

            int max = city.PointOfInterestDTOs.Max(poi=>poi.Id);
            PointOfInterestDTO NewpointOfInterest = new PointOfInterestDTO()
            {
                Id = ++max,
                Name = creationPointOfInterestDTO.Name,
                Description = creationPointOfInterestDTO.Description
            };
            city.PointOfInterestDTOs.Add(NewpointOfInterest);
            return CreatedAtRoute("GetPointOfInterest",new
            {
                cityId=cityId,
                poiId=max
            },
            NewpointOfInterest
            );
        }

        [HttpPut("{poiId}")]
        public ActionResult PutUpdateOnPointOfInterest(int cityId, int poiId,UpdatePointOfInterestDTO updatePointOfInterest) {

            CityDTO city = _dataSource.Cities.Where(city => city.Id == cityId).FirstOrDefault();
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
            pointOfInterest.Name = updatePointOfInterest.Name;
            pointOfInterest.Description = updatePointOfInterest.Description;
            return NoContent();
        }

        // For patch we need to install this Microsoft.AspNetCore.JsonPatch and Microsoft.AspNetCore.Mvc.NewtonsoftJson packages and Add  AddNewtonsoftJson to the Service in the Program.cs 
        [HttpPatch("{poiId}")]
        public ActionResult PatchPointOfInterest(int cityId,int poiId,JsonPatchDocument<UpdatePointOfInterestDTO> patchDocument)
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
            PointOfInterestDTO pointOfInterestFromDb = city.PointOfInterestDTOs.Where(poi => poi.Id == poiId).FirstOrDefault();
            if (pointOfInterestFromDb is null)
            {
                return NotFound();
            }
            UpdatePointOfInterestDTO updatePointOfInterest=new UpdatePointOfInterestDTO
            {
                Name = pointOfInterestFromDb.Name,
                Description = pointOfInterestFromDb.Description
            };
            patchDocument.ApplyTo(updatePointOfInterest, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!TryValidateModel(updatePointOfInterest))
            {
                return BadRequest(ModelState);
            }
            pointOfInterestFromDb.Name = updatePointOfInterest.Name;
            pointOfInterestFromDb.Description = updatePointOfInterest.Description;


            return NoContent();
        }

        [HttpDelete("{poiId}")]
        public ActionResult DeletePointOfInterest(int cityId, int poiId)
        {
            CityDTO city = _dataSource.Cities.Where(city => city.Id == cityId).FirstOrDefault();
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
            city.PointOfInterestDTOs.Remove(pointOfInterest);
            _mailservice.SendMail($"PointOfInterest with id {poiId} deleted successfully !");
            return NoContent();
        }

    }
}
