using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebNote.MVC6.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _environment;  // DI, folder access

        public UploadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost, Route("api/upload")]   // Route - {Upload/ImageUpload -> api/upload} url path change
        public async Task<IActionResult> ImageUpload(IFormFile file)    // file - input image or file
        {
            // image or file upload
            // 1. path set
            var path = Path.Combine(_environment.WebRootPath, @"images\upload");

            // 2. file name set - normaly use DateTime + GUID
            // 3. extension - jpg, png, etc
            var fileFullName = file.FileName.Split('.');
            var fileName = $"{Guid.NewGuid()}.{fileFullName[1]}";

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Ok(new { file = "/images/upload/" + fileName, success=true });

            // # URL access method
            // ASP.NET - HOSTname/ + api/upload
            // JavaScript - HOSTname + api/upload <- http://www.example.comapi/upload (wrong) add / you must
        }
    }
}
