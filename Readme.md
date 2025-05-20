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
* Created CityDataSource Class for temporary list of cityDto.
* Created DTO for PointOfInterest
* Used ActionResult as return type
* Created Controllers for city,PointOfInterest and also for File
* Also set the acceptable formatters like only json and xml. If not response as 406 Not Acceptable status code
* Implemented File Controller and return a pdf file as response using FileExtensionContentTypeProvider Class .Added this class to  the DI container as singleton so that it will create object only once.
* This class is used to get the Contenttype which is the file type dynamically.
---------------------------------------End of Section 3 _________________________________________________________________________________________________
