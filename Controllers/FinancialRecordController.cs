using FinanceDashboardAPI.Interfaces;
using FinanceDashboardAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceDashboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialRecordController : ControllerBase
    {
        private readonly IFinancialRecordService _service;

        public FinancialRecordController(IFinancialRecordService service)
        {
            _service = service;
        }

        // Create Record
        [Authorize]
        [HttpPost("Create Record")]
        public async Task<IActionResult> Create(CreateRecordDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _service.AddRecord(dto, userId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("my-records")]
        public async Task<IActionResult> GetMyRecords()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var records = await _service.GetRecordsByUserId(userId);
            return Ok(records);
        }


        // Admin → Get all records
        [Authorize(Roles = "Admin")]
        [HttpGet("Get Records")]
        public async Task<IActionResult> GetAll()
        {
            var records = await _service.GetAllRecords();
            return Ok(records);
        }

        // Admin → Get record by Id
        [Authorize(Roles = "Admin")]
        [HttpGet("{recordid}")]
        public async Task<IActionResult> GetById(int recordid)
        {
            var record = await _service.GetRecordById(recordid);

            if (record == null)
                return NotFound("Record not found");

            return Ok(record);
        }

        // Admin → Get record by UserId
        [Authorize(Roles = "Admin")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var records = await _service.GetRecordsByUserId(userId);
            return Ok(records);
        }

        // Admin → Update ANY record
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRecordDTO dto)
        {
            var result = await _service.UpdateRecord(id, dto);

            if (result.Contains("not found"))
                return NotFound(result);

            return Ok(result);
        }

        // Admin → Delete ANY record
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteRecord(id);

            if (result.Contains("not found"))
                return NotFound(result);

            return Ok(result);
        }
    }
}
