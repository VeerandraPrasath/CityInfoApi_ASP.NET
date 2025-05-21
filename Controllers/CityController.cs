using AutoMapper;
using CityInfoApi.Entity;
using CityInfoApi.Models;
using CityInfoApi.Service;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace CityInfoApi.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CityController:ControllerBase
    {
        //private CityDataSource _dataSource;
        private ICityInfoApiRepository _databaseRepository;
        private IMapper _mapper;
        public CityController(ICityInfoApiRepository databaseRepository,IMapper mapper)
        {
            //_dataSource = dataSource;
            _databaseRepository = databaseRepository;
            _mapper = mapper;

        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCities()
        {
            //return Ok(CityDataSource.Current.Cities);

            var cities =await _databaseRepository.GetCitiesAsync();
            if (cities is null)
            {
                return NotFound();
            }

            //List<CityDTO> citiesToReturn = new List<CityDTO>();
            //foreach (CityEntity city in cities)
            //{
            //    citiesToReturn.Add(new CityDTO
            //    {
            //        Id = city.Id,
            //        Name = city.Name,
            //        Description = city.Description
            //    });
            //}

             return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDTO>>(cities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCity(int id)
        {
            CityEntity city= await  _databaseRepository.GetCityAsync(id);
            if(city is null)
            {
                return NotFound();
            }
            //CityDTO cityToReturn = new CityDTO
            //{
            //    Id = city.Id,
            //    Name = city.Name,
            //    Description = city.Description
            //};
            //return Ok(cityToReturn);
            return Ok(_mapper.Map<CityWithoutPointOfInterestDTO>(city));
        }

    }
}
