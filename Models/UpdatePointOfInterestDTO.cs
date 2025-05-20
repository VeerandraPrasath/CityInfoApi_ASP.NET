using System.ComponentModel.DataAnnotations;

namespace CityInfoApi.Models
{
    public class UpdatePointOfInterestDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string  Name { get; set; }    

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}

