using FinanceDashboardAPI.Interfaces;
using FinanceDashboardAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDashboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AdminController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(CreateUserByAdminDTO dto)
        {
            var result = await _authService.CreateUserByAdmin(dto);

            if (result.Contains("exists") || result.Contains("Invalid"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authService.GetAllUsers();
            return Ok(users);
        }

        [HttpPut("toggle-user/{userid}")]
        public async Task<IActionResult> ToggleUser(int userid)
        {
            var result = await _authService.ToggleUserStatus(userid);

            if (result.Contains("not found"))
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("update-role/{userid}")]
        public async Task<IActionResult> UpdateRole(int userid, UpdateUserRoleDTO dto)
        {
            var result = await _authService.UpdateUserRole(userid, dto);

            if (result.Contains("not found") || result.Contains("Invalid"))
                return BadRequest(result);

            return Ok(result);
        }
    }
}