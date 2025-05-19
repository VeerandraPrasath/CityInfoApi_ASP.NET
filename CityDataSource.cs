using CityInfoApi.Models;

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
                Name="Prasath",
                Description="Prasath is a software engineer"
            },
            new CityDTO
            {
                Id=2,
                Name="Arun",
                Description="Arun is a software engineer"
            },
            new CityDTO
            {
                Id=3,
                Name="Siva",
                Description="Siva is a software engineer"
            },
            new CityDTO
            {
                Id=4,
                Name="Vasanth",
                Description="Vasanth is a software engineer"
            },
        };
        }
    }
}
