using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfoApi.Entity
{
    public class CityEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot be more than 100 characters.")]
        public string Name { get; set; }


        [MaxLength(100, ErrorMessage = "Description cannot be more than 100 characters.")]
        public string Description { get; set; }


        public ICollection<PointOfInterestEntity> PointOfInterestEntities { get; set; } = new List<PointOfInterestEntity>();//Also act as Navigational Property
    }
}
