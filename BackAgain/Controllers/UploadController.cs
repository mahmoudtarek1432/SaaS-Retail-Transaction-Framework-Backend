using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackAgain.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackAgain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IHostingEnvironment _webHost;

        public UploadController(IHostingEnvironment webhost)
        {
            _webHost = webhost;
        }

        [HttpPost("")]
        public ActionResult<ClientResponseManager<string>> UpdateIcon()
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (User != null)
                {
                    var icon = Request.Form.Files[0];
                    if(icon.Length > 0)
                    {
                        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                        fileName = Guid.NewGuid().ToString() + "_" + icon.FileName;
                        string uploadFolderPath = Path.Combine(_webHost.WebRootPath, "Images");
                        string filePath = Path.Combine(uploadFolderPath, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            icon.CopyTo(fileStream);
                        }

                        return Ok(new ClientResponseManager<string>
                        {
                            ResponseObject = fileName,
                            IsSuccessfull = true,
                            Message = "image Uploaded Successful, ${fileName}"
                        });
                    }
                }
                return BadRequest(new ClientResponseManager<string>
                {

                    IsSuccessfull = false,
                    Message = "Login token is not valid"
                });
            }
            return BadRequest(new ClientResponseManager<string>
            {
                IsSuccessfull = false,
                Message = "file corupted"
            });
        }
    }
}