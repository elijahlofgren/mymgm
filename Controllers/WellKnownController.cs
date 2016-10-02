using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace mymgm.Controllers
{
    // Begin code to make LetsEncrypt work from http://www.westerndevs.com/Tools/Lets-Encrypt-ASPNET-Core/
    // Asking about license here: https://twitter.com/elijahlofgren/status/782594810901397504
    [Route(".well-known")]
    public class WellKnownController : Controller
    {
        public WellKnownController(IHostingEnvironment env)
        {
            Env = env;
        }

        private IHostingEnvironment Env { get; }

        [HttpGet("acme-challenge/{id}")]
        [Produces("text/json")]
        public IActionResult Get(string id)
        {
            var content = string.Empty;
            var path = Env.WebRootPath;
            var fullPath = Path.Combine(path, @".well-known\acme-challenge");
            var dirInfo = new DirectoryInfo(fullPath);
            var files = dirInfo.GetFiles();
            foreach (var fileInfo in files)
            {
                if (fileInfo.Name == id)
                {
                    using (var file = System.IO.File.OpenText(fileInfo.FullName))
                    {
                        return Ok(file.ReadToEnd());
                    }
                }
            }
            return Ok(content);
        }
    }
    // End code to make LetsEncrypt work from http://www.westerndevs.com/Tools/Lets-Encrypt-ASPNET-Core/
    // Asking about license here: https://twitter.com/elijahlofgren/status/782594810901397504

}