## Section 3
* Added UseRouting ,UseEndpoints and MapControllers to the pipeline
* Added CityController Class and extends from ControllerBase
* Set ApiController attribute to CityController
* Set Base Route to CityController as /api/cities
* Added GetCity method to return a list of cities
* Set HttpGet attribute to GetCity method
* Retruned JsonResut with a list of cities and check the action which means method in SwaggerUi and also in the postman.
* Turned off SSL certification check in Postman
### Data Transfer Object
* Create DTO model for City
* Created CityDataSource Class for temperory list of cityDto
