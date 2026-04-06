using FinanceDashboardAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceDashboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        // USER DASHBOARD
        [Authorize]
        [HttpGet("my-summary")]
        public async Task<IActionResult> GetMyDashboard()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _service.GetUserDashboard(userId);
            return Ok(result);
        }

        // ADMIN DASHBOARD
        [Authorize(Roles = "Admin")]
        [HttpGet("all-summary")]
        public async Task<IActionResult> GetAllDashboard()
        {
            var result = await _service.GetAllDashboard();
            return Ok(result);
        }
    }
}
