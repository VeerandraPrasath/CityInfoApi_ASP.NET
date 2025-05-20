using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfoApi.Controllers
{
    [ApiController]
    [Route("api/file")]
    public class FileController :ControllerBase
    {
        private FileExtensionContentTypeProvider _fileExtensionTypeProvider;

        public FileController(FileExtensionContentTypeProvider fileExtensionTypeProvider)
        {
            _fileExtensionTypeProvider = fileExtensionTypeProvider;
        }
        [HttpGet()]
        public ActionResult GetFile()
        {
            //bool a= System.IO.File.Exists("getting - started - with - rest - slides.pdf");\
            string path = "C:\\Users\\veerandra.prasath\\source\\repos\\CityInfoApi\\getting-started-with-rest-slides.pdf";

            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }
            
            if(!_fileExtensionTypeProvider.TryGetContentType(path,out string contentType))
            {
                contentType = "application/octet-stream"; //Default type
            }

            byte[] FileInBytes= System.IO.File.ReadAllBytes(path);


            //return Ok(File(FileInBytes, "text/plain", "Prasath-AutoBiography.pdf")); //It only return as Application/Json.
            return File(FileInBytes,contentType, "Prasath-AutoBiography.pdf");
        }

    }
}
