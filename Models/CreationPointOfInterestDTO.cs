using System.ComponentModel.DataAnnotations;

namespace CityInfoApi.Models
{
    public class CreationPointOfInterestDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
