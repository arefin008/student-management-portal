using BLL.Services;
using DAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace studentManagement.Controllers
{
    [ApiController]
    [Route("api/results")]
    [Authorize]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _svc;
        public ResultController(IResultService svc) => _svc = svc;

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var result = await _svc.GetByStudentIdAsync(studentId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ResultCreateDto dto)
        {
            if (dto.Marks < 0 || dto.Marks > 100)
                return BadRequest(new { message = "Marks must be between 0 and 100." });
            var id = await _svc.CreateAsync(dto);
            return Ok(new { id });
        }
    }
}