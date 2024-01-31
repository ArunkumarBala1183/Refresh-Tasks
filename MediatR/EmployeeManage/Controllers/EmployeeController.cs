using MediatorLibrary.Repository.Models;
using MediatorLibrary.Repository.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator mediatr;
        public EmployeeController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var response = await this.mediatr.Send(new ViewEmployeeListQuery());
            return Ok(response);
        }
    }
}