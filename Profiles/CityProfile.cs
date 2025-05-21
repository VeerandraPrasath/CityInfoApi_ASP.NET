using AutoMapper;
using CityInfoApi.Entity;
using CityInfoApi.Models;

namespace CityInfoApi.Profiles
{
    public class CityProfile:Profile
    {
        public CityProfile()
        {
            CreateMap<CityEntity,CityWithoutPointOfInterestDTO>();
        }
    }
}
