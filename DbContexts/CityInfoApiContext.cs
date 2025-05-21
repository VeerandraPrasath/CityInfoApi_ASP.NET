using CityInfoApi.Entity;
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
    }
}
