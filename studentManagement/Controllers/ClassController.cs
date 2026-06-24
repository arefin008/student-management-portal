using BLL.Services;
using DAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace studentManagement.Controllers
{
    [ApiController]
    [Route("api/classes")]
    [Authorize]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _svc;
        public ClassController(IClassService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ClassDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ClassName))
                return BadRequest(new { message = "Class name is required." });
            var id = await _svc.CreateAsync(dto.ClassName);
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ClassDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ClassName))
                return BadRequest(new { message = "Class name is required." });
            var ok = await _svc.UpdateAsync(id, dto.ClassName);
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