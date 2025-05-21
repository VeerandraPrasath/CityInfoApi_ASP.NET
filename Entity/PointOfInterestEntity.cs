using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfoApi.Entity
{
    public class PointOfInterestEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot be more than 100 characters.")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "Description cannot be more than 100 characters.")]
        public string Description { get; set; }

        [ForeignKey("CityId")]
        public int CityId { get; set; }

        public CityEntity City { get; set; }  //Naviagtion Property
    }
}