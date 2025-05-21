using AutoMapper;
using CityInfoApi.Entity;
using CityInfoApi.Models;

namespace CityInfoApi.Profiles
{
    public class PointOfInterestProfile:Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<PointOfInterestEntity,PointOfInterestDTO>();
        }
    }
}
