namespace CityInfoApi.Models
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public ICollection<PointOfInterestDTO> PointOfInterestDTOs { get; set; }
        public ICollection<PointOfInterestDTO> PointOfInterestDTOs { get; set; } = new List<PointOfInterestDTO>(); 

        public CityDTO()
        {
            //PointOfInterestDTOs = new List<PointOfInterestDTO>();
        }
    }
}
