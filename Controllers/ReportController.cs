using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlantsDetection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        private readonly PlantsDetectionContext _context;

        public ReportController(PlantsDetectionContext context)
        {
            _context = context;
        }

        //// GET api/report/get-my-reports
        //[HttpGet("get-my-reports")]
        //public async Task<ActionResult<IEnumerable<Report>>> GetMyReports()
        //{
        //    var userId = GetUserIdFromToken(); // Implement this method to get the current user's ID from the JWT token

        //    var reports = await _context.Reports.Where(r => r.UserId == userId).ToListAsync();

        //    return reports;
        //}

        //// GET api/report
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        //{
        //    var isAdmin = IsUserAdmin(); // Implement this method to check if the current user is an admin

        //    if (!isAdmin)
        //    {
        //        return Forbid(); // Return 403 Forbidden if the user is not an admin
        //    }

        //    var reports = await _context.Reports.ToListAsync();

        //    return reports;
        //}

        //// GET api/report/id
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Report>> GetReport(int id)
        //{
        //    var report = await _context.Reports.FindAsync(id);

        //    if (report == null)
        //    {
        //        return NotFound();
        //    }

        //    var isAdmin = IsUserAdmin(); // Implement this method to check if the current user is an admin
        //    var isOwner = report.UserId == GetUserIdFromToken(); // Implement this method to get the current user's ID from the JWT token

        //    if (!isAdmin && !isOwner)
        //    {
        //        return Forbid(); // Return 403 Forbidden if the user is neither an admin nor the owner of the report
        //    }

        //    return report;
        //}

        //// POST api/report
        //[HttpPost]
        //public async Task<ActionResult<Report>> CreateReport(Report report)
        //{
        //    // Set the user ID for the report
        //    report.UserId = GetUserIdFromToken(); // Implement this method to get the current user's ID from the JWT token

        //    _context.Reports.Add(report);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetReport), new { id = report.Id }, report);
        //}

        // Implement helper methods for authentication, such as GetUserIdFromToken and IsUserAdmin

        // Implement other CRUD operations (PUT, DELETE) as needed
    }
}
