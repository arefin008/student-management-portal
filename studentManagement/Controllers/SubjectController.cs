using BLL.Services;
using DAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace studentManagement.Controllers
{
    [ApiController]
    [Route("api/subjects")]
    [Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _svc;
        public SubjectController(ISubjectService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] SubjectDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.SubjectName))
                return BadRequest(new { message = "Subject name is required." });
            var id = await _svc.CreateAsync(dto.SubjectName);
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] SubjectDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.SubjectName))
                return BadRequest(new { message = "Subject name is required." });
            var ok = await _svc.UpdateAsync(id, dto.SubjectName);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _svc.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}