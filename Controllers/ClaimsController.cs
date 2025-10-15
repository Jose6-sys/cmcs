using System.IO;
using System.Linq;
using cmcs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cmcs.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ClaimsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //  Lecturer submits a claim
        [HttpPost]
        public IActionResult SubmitClaim(double hoursWorked, double hourlyRate, string notes, IFormFile file)
        {
            // Get the logged-in user's email from session
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
            {
                TempData["Error"] = "User not found. Please log in again.";
                return RedirectToAction("Index", "Home");
            }

            var claim = new Claim
            {
                UserId = user.UserId,
                HoursWorked = hoursWorked,
                HourlyRate = hourlyRate,
                Notes = notes,
                Status = "Pending"
            };

            _context.Claims.Add(claim);
            _context.SaveChanges();

            if (file != null)
            {
                string uploads = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);
                string filePath = Path.Combine(uploads, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var claimFile = new ClaimFile
                {
                    ClaimId = claim.ClaimId,
                    FileName = file.FileName,
                    FilePath = "/uploads/" + file.FileName
                };
                _context.ClaimFiles.Add(claimFile);
                _context.SaveChanges();
            }

            TempData["Success"] = "Claim submitted successfully!";
            return RedirectToAction("MyClaims", "Home");
        }

        //  Used by coordinator/manager to update claim status
        [HttpPost]
        public IActionResult UpdateStatus(int claimId, string status)
        {
            var claim = _context.Claims.Find(claimId);
            if (claim != null)
            {
                claim.Status = status;
                _context.SaveChanges();
            }

            return RedirectToAction("CoordinatorClaims", "Home");
        }
    }
}
