using CityInfoApi.Entity;
using CityInfoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfoApi.DbContexts
{
    public class CityInfoApiContext:DbContext
    {
        public DbSet<CityEntity> Cities { get; set; }

        public DbSet<PointOfInterestEntity> PointOfInterests { get; set; }

        public CityInfoApiContext(DbContextOptions<CityInfoApiContext> options):base(options)
        {

        }

        //One way of configuring the DbContext
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=CityInfoApi.db");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityEntity>().
                HasData(new CityEntity
                {
                    Id = 1,
                    Name = "Madurai",
                    Description = "Madurai is famous for everything",
                 
                },
            new CityEntity
            {
                Id = 2,
                Name = "Coimbatore",
                Description = "Coimbatore is famous for Slang"
            },
            new CityEntity
            {
                Id = 3,
                Name = "Theni",
                Description = "Theni is a place of nature"
            });
            modelBuilder.Entity<PointOfInterestEntity>().
             HasData(new PointOfInterestEntity
             {
                 Id = 1,
                 Name = "Vadipatti",
                 Description = "Vadipatti is a small town in Madurai district",
                 CityId = 1
             },
                new PointOfInterestEntity
                {
                    Id = 2,
                    Name = "Solavanthan",
                    Description = "Madurai is a city in Tamil Nadu",
                    CityId = 1
                },
                 new PointOfInterestEntity
                 {
                     Id = 3,
                     Name = "Pollachi",
                     Description = "Pollachi is for Kongu culture and traditions",
                     CityId = 2
                 },
                    new PointOfInterestEntity
                    {
                        Id = 4,
                        Name = "Metupalayam",
                        Description = "Metupalayam is a small town in Coimbatore district",
                        CityId = 2
                    },
                     new PointOfInterestEntity
                     {
                         Id = 5,
                         Name = "Cumbam",
                         Description = "Cumbam is a small town in Theni district where sree praveen lives",
                         CityId = 3
                     },
                    new PointOfInterestEntity
                    {
                        Id = 6,
                        Name = "Chinnamanur",
                        Description = "Chinnamanur is a small town in Theni district where sudhan siva lives",
                        CityId = 3
                    }
                );

         base.OnModelCreating(modelBuilder);
        }
    }
}
