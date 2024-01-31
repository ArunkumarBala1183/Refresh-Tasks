using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService service)
        {
            employeeService = service;
        }

        [HttpPost("NewEmployee")]
        public async Task<IActionResult> NewEmployee(EmployeeDto employeeDetails)
        {
            var response = await employeeService.NewEmployee(employeeDetails);
            return Ok(response);
        }

        [HttpPut("EditEmployee")]
        public async Task<IActionResult> EditEmployee(EmployeeDto employeeDetails)
        {
            var response = await employeeService.UpdateEmployee(employeeDetails);
            return Ok(response);
        }

        //[AllowAnonymous]
        [HttpGet("ViewEmployees")]
        public async Task<IActionResult> ViewEmployees()
        {
            var response = await employeeService.ViewEmployees();
            return Ok(response);
        }

        [HttpGet("GetEmployee/{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var response = await employeeService.GetEmployee(id);
            return Ok(response);
        }

        [HttpDelete("RemoveEmployee/{id}")]
        public async Task<IActionResult> RemoveEmployee(int id)
        {
            var response = await employeeService.DeleteEmployee(id);
            return Ok(response);
        }

        [HttpPost("RoleUpdate/{id}")]
        public async Task<IActionResult> RoleUpdate(int id , List<EmployeeRoleDto> roles)
        {
            var response = await employeeService.RoleUpdate(id , roles);
            return Ok(response);
        }

    }
}
