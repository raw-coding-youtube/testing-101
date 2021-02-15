using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SampleApp.Controllers
{
    [ApiController]
    [Route("/api/files")]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly FileSettings _settings;

        public FileController(
            IWebHostEnvironment env,
            IOptionsMonitor<FileSettings> optionsMonitor
        )
        {
            _env = env;
            _settings = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        public async Task<IActionResult> SaveFile([FromForm] IFormFile file)
        {
            var savePath = Path.Combine(_env.WebRootPath, _settings.Path);
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            var fileSavePath = Path.Combine(savePath, file.FileName);
            await using var fileStream = System.IO.File.Create(fileSavePath);
            await file.CopyToAsync(fileStream);
            return Ok();
        }
    }
}