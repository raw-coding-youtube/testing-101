using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RawCoding.WebApp.Controllers
{
    [ApiController]
    public class SomeController : ControllerBase
    {
        [Route("/public")]
        public string Public() => "Public";

        [Route("/secure")]
        [Authorize]
        public string Secure() => "Secure";

        [Route("/privileged")]
        [Authorize(Policy = "Privileged")]
        public string Privileged() => "Privileged";

        [Route("/admin")]
        [Authorize(Policy = "Administrator")]
        public string Admin() => "Admin";
    }
}