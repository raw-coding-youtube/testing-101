using Microsoft.AspNetCore.Mvc;
using SampleApp.Services;

namespace SampleApp.Controllers
{
    [ApiController]
    [Route("/api/animals")]
    public class AnimalController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAnimals([FromServices] IAnimalService service)
        {
            return Ok(service.GetAnimal());
        }
    }
}