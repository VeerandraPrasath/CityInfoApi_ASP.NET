using CityInfoApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace CityInfoApi
{
    public class CityDataSource
    {
        public static CityDataSource Current=new CityDataSource();
        public List<CityDTO> Cities { get; set; }
        public CityDataSource()
        {



            Cities = new List<CityDTO>
           {
            new CityDTO
            {
                Id=1,
                Name="Madurai",
                Description="Madurai is famous for everything",
                PointOfInterests = new List<PointOfInterestDTO>{
                new PointOfInterestDTO
                {
                    Id=1,
                    Name="Vadipatti",
                    Description="Vadipatti is a small town in Madurai district"
                },
                new PointOfInterestDTO
                {
                    Id=2,
                    Name="Solavanthan",
                    Description="Madurai is a city in Tamil Nadu"
                }
                }
            },
            new CityDTO
            {
                Id=2,
                Name="Coimbatore",
                Description="Coimbatore is famous for Slang",
                PointOfInterests = new List<PointOfInterestDTO>{
                    new PointOfInterestDTO
                    {
                        Id=1,
                        Name="Pollachi",
                        Description="Pollachi is for Kongu culture and traditions"
                    },
                    new PointOfInterestDTO
                    {
                        Id=2,
                        Name="Metupalayam",
                        Description="Metupalayam is a small town in Coimbatore district"
                    }
                }
            },
            new CityDTO
            {
                Id=3,
                Name="Theni",
                Description="Theni is a place of nature",
                PointOfInterests = new List<PointOfInterestDTO>{
                    new PointOfInterestDTO
                    {
                        Id=1,
                        Name="Cumbam",
                        Description="Cumbam is a small town in Theni district where sree praveen lives"
                    },
                    new PointOfInterestDTO
                    {
                        Id=2,
                        Name="Chinnamanur",
                        Description="Chinnamanur is a small town in Theni district where sudhan siva lives"
                    }
                }
            }
        };
        }
    }
}
