using AutoMapper;
using CityInfoApi.Entity;
using CityInfoApi.MailService;
using CityInfoApi.Models;
using CityInfoApi.Service;
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
        //private CityDataSource _dataSource;
        private ICityInfoApiRepository _databaseRepository;
        private IMailService _mailservice;
        private IMapper _mapper;

        public PointOfInterestController(ICityInfoApiRepository databaseRepository,IMailService mailService,IMapper mapper)
        {
            //_dataSource = dataSource;
            _databaseRepository = databaseRepository;
            _mailservice = mailService;
            _mapper=mapper;
            //_logger = logger;
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<PointOfInterestDTO>>> GetPointOfInterests(int cityId)
        {
            try
            {
                //This thorow is for catch the the exception and log the critical info in the console.
                //throw new Exception("Test Exception for logging critical infos");

                //CityDTO city = _dataSource.Cities.Where(city => city.Id == cityId).FirstOrDefault();

                if(! await _databaseRepository.CityExitAsync(cityId))
                {
                    return NotFound(); //203 status code
                }
                //if (city is null)
                //{
                //    //_logger.LogInformation($"City with id {cityId} was not found when accessing point of interest");-->commented because we are using serilogger.
                //    return NotFound(); //203 status code
                //}
                IEnumerable<PointOfInterestEntity> pointOfInterests = await _databaseRepository.GetPointOfInterestsForCityAsync(cityId);

                if (pointOfInterests==null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<IEnumerable<PointOfInterestDTO>>(pointOfInterests));
            }
            catch (Exception ex)
            {
                //_logger.LogCritical($"Exception while getting point of interest for city with id {cityId}.", ex); //We only mention to log informaton in the Launchsettings.json but the critical things are also logging in the console.
                return StatusCode(500, "A problem happened while handling your request.");

            }

        }

        [HttpGet("{poiId}",Name ="GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDTO>> GetPointOfInterest(int cityId, int poiId)
        {
            if(! await _databaseRepository.CityExitAsync(cityId))
            {
                return NotFound(); //203 status code
            }
            //CityDTO city = _dataSource.Cities.Where(city => city.Id == cityId).FirstOrDefault();
            PointOfInterestEntity pointOfInterest = await _databaseRepository.GetPointOfInterestForCityAsync(cityId, poiId);
            if (pointOfInterest == null)
            {
                return NotFound(); //203 status code
            }
            return Ok(_mapper.Map<PointOfInterestDTO>(pointOfInterest));
        }

        [HttpPost()]
        public async Task<IActionResult> CreatePointOfInterest(int cityId, CreationPointOfInterestDTO creationPointOfInterestDTO)
        {
            //CityDTO city = _dataSource.Cities.Where(city => city.Id == cityId).FirstOrDefault();
            //if(city is null)
            //{
            //    return NotFound(); //203 status code
            //}

            if (!_databaseRepository.CityExitAsync(cityId).Result)
            {
                return NotFound(); //203 status code
            }


            //int max = city.PointOfInterests.Max(poi=>poi.Id); the id is primary key.So the DB autogenerate and set the value.
            //PointOfInterestDTO NewpointOfInterest = new PointOfInterestDTO()
            //{
            //    Id = ++max,
            //    Name = creationPointOfInterestDTO.Name,
            //    Description = creationPointOfInterestDTO.Description
            //};
            //city.PointOfInterests.Add(NewpointOfInterest);

            PointOfInterestEntity pointOfInterest = _mapper.Map<PointOfInterestEntity>(creationPointOfInterestDTO);
            await _databaseRepository.AddPointOfInterestForCityAsync(cityId, pointOfInterest);
            await _databaseRepository.SaveAsync();

            PointOfInterestDTO pointOfInterestDTO = _mapper.Map<PointOfInterestDTO>(pointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",new
            {
                cityId=cityId,
                poiId=pointOfInterestDTO.Id
            },
            pointOfInterestDTO
            );
        }

        //[HttpPut("{poiId}")]
        //public ActionResult PutUpdateOnPointOfInterest(int cityId, int poiId,UpdatePointOfInterestDTO updatePointOfInterest) {

        //    CityDTO city = _dataSource.Cities.Where(city => city.Id == cityId).FirstOrDefault();
        //    if (city is null)
        //    {
        //        return NotFound(); //203 status code
        //    }
        //    if (city.PointOfInterests.Count == 0)
        //    {
        //        return NotFound();
        //    }
        //    PointOfInterestDTO pointOfInterest = city.PointOfInterests.Where(poi => poi.Id == poiId).FirstOrDefault();
        //    if (pointOfInterest is null)
        //    {
        //        return NotFound();
        //    }
        //    pointOfInterest.Name = updatePointOfInterest.Name;
        //    pointOfInterest.Description = updatePointOfInterest.Description;
        //    return NoContent();
        //}

        //// For patch we need to install this Microsoft.AspNetCore.JsonPatch and Microsoft.AspNetCore.Mvc.NewtonsoftJson packages and Add  AddNewtonsoftJson to the Service in the Program.cs 
        //[HttpPatch("{poiId}")]
        //public ActionResult PatchPointOfInterest(int cityId,int poiId,JsonPatchDocument<UpdatePointOfInterestDTO> patchDocument)
        //{
        //    CityDTO city = CityDataSource.Current.Cities.Where(city => city.Id == cityId).FirstOrDefault();
        //    if (city is null)
        //    {
        //        return NotFound(); //203 status code
        //    }
        //    if (city.PointOfInterests.Count == 0)
        //    {
        //        return NotFound();
        //    }
        //    PointOfInterestDTO pointOfInterestFromDb = city.PointOfInterests.Where(poi => poi.Id == poiId).FirstOrDefault();
        //    if (pointOfInterestFromDb is null)
        //    {
        //        return NotFound();
        //    }
        //    UpdatePointOfInterestDTO updatePointOfInterest=new UpdatePointOfInterestDTO
        //    {
        //        Name = pointOfInterestFromDb.Name,
        //        Description = pointOfInterestFromDb.Description
        //    };
        //    patchDocument.ApplyTo(updatePointOfInterest, ModelState);
        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    if (!TryValidateModel(updatePointOfInterest))
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    pointOfInterestFromDb.Name = updatePointOfInterest.Name;
        //    pointOfInterestFromDb.Description = updatePointOfInterest.Description;


        //    return NoContent();
        //}

        //[HttpDelete("{poiId}")]
        //public ActionResult DeletePointOfInterest(int cityId, int poiId)
        //{
        //    CityDTO city = _dataSource.Cities.Where(city => city.Id == cityId).FirstOrDefault();
        //    if (city is null)
        //    {
        //        return NotFound(); //203 status code
        //    }
        //    if (city.PointOfInterests.Count == 0)
        //    {
        //        return NotFound();
        //    }
        //    PointOfInterestDTO pointOfInterest = city.PointOfInterests.Where(poi => poi.Id == poiId).FirstOrDefault();
        //    if (pointOfInterest is null)
        //    {
        //        return NotFound();
        //    }
        //    city.PointOfInterests.Remove(pointOfInterest);
        //    _mailservice.SendMail($"PointOfInterest with id {poiId} deleted successfully !");
        //    return NoContent();
        //}

    }
}
