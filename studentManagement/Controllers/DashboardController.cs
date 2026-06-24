
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace studentManagement.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _svc;
        public DashboardController(IDashboardService svc) => _svc = svc;

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats() => Ok(await _svc.GetStatsAsync());
    }
}