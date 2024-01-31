using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Models.DTOs;
using EmployeeManagement.Repository.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleService roleService;
        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;     
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(RoleDto role)
        {
            HttpStatusCode response = await this.roleService.NewRole(role);

            if (response.Equals(HttpStatusCode.Created))
            {
                return Created();
            }
            else
            {
                return StatusCode((int)response);
            }
            
        }

        [HttpGet("ViewRoles")]
        public async Task<IActionResult> ViewRoles()
        {
            var response = await this.roleService.ViewRoles();
            
            if(response.GetType() != typeof(HttpStatusCode))
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var response = await this.roleService.GetRole(id);

            if (response.GetType() != typeof(HttpStatusCode))
            {
                return Ok(new {role = response});
            }
            else
            {
                return StatusCode((int)response);
            }
        }

        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole(RoleDto role)
        {
            var response = await this.roleService.UpdateRole(role);
            
            if(response.Equals(HttpStatusCode.Created)) 
            {
                return Created();
            }
            else
            {
                return StatusCode((int)response);
            }
        }

        [HttpDelete("RemoveRole/{id}")]
        public async Task<IActionResult> RemoveRole(int id)
        {
            var response = await this.roleService.RemoveRole(id);

            if(response.Equals(HttpStatusCode.NoContent))
            {
                return Created();
            }
            else if(response.Equals(HttpStatusCode.NotFound))
            {
                return NotFound();
            }
            else
            {
                return StatusCode((int)response);
            }
        }

    }
}
