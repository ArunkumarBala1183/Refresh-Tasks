using System.Net;
using EfInheritance.Repository.DbModels;
using EfInheritance.Repository.Services;
using Microsoft.AspNetCore.Mvc;

namespace EfInheritance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TPHController : ControllerBase
    {
        private readonly ICarService carService;
        private readonly IBikeServices bikeServices;
        public TPHController(ICarService service, IBikeServices bikeServices)
        {
            this.carService = service;
            this.bikeServices = bikeServices;
        }

        [HttpPost("AddCarDetails")]
        public async Task<IActionResult> AddCarDetails([FromBody] Car carDetails)
        {
            var response = await this.carService.addCarDetails(carDetails);

            if (response == HttpStatusCode.Created)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("ViewCarDetails")]
        public async Task<IActionResult> ViewCarDetails()
        {
            var response = await this.carService.viewCarDetails();

            if (response != null)
            {
                if (response.GetType() != typeof(HttpStatusCode))
                {
                    return Ok(response);
                }

                return NotFound();
            }
            else
            {
                return NoContent();

            }
        }

        [HttpPost("AddBikeDetails")]
        public async Task<IActionResult> AddBikeDetails([FromBody] Bike bikeDetails)
        {
            var response = await this.bikeServices.addBikeDetails(bikeDetails);

            if (response == HttpStatusCode.Created)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("ViewBikeDetails")]
        public async Task<IActionResult> ViewBikeDetails()
        {
            var response = await this.bikeServices.viewBikeDetails();

            if (response != null)
            {
                if (response.GetType() != typeof(HttpStatusCode))
                {
                    return Ok(response);
                }

                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }
    }
}