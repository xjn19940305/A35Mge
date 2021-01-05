using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UEditor.Core;

namespace A35Mge.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "manager")]
    public class UeController : ControllerBase
    {
        private readonly UEditorService ue;

        public UeController(UEditorService uEditorService)
        {
            this.ue = uEditorService;
        }
        [Route("api/Do")]
        [HttpGet, HttpPost]
        public ContentResult Do()
        {
            var response = ue.UploadAndGetResponse(HttpContext);
            return Content(response.Result, response.ContentType);
        }
        [Route("api/UploadImg")]
        [HttpPost]
        public async Task<IActionResult> UploadImg([FromForm] IFormFile avatar)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "upload");
            path = Path.Combine(path, "OtherImg");
            path = Path.Combine(path, $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var extend = Path.GetExtension(avatar.FileName);
            var fileName = Guid.NewGuid() + extend;

            var filePath = path + Path.DirectorySeparatorChar + fileName;
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await avatar.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            var currentPathFile = Path.DirectorySeparatorChar + "upload" + Path.DirectorySeparatorChar + "OtherImg" + Path.DirectorySeparatorChar + $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}" + Path.DirectorySeparatorChar + fileName;
            return Ok(currentPathFile);
        }
    }
}
