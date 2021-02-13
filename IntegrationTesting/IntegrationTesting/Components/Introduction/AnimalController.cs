using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTesting.Components.Introduction
{
    public class AnimalController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public AnimalController(AppDbContext ctx)
        {
            _ctx = ctx;
        }


        [HttpGet]
        public IActionResult List()
        {
            return Ok(_ctx.Animals.ToList());
        }
    }
}