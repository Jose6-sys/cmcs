using System.Linq;
using cmcs.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cmcs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View(); // Login view

        public IActionResult Register() => View();

        public IActionResult LecturerForm()
        {
            return View();
        }

        public IActionResult CoordinatorClaims()
        {
            var claims = _context.Claims
                .Include(c => c.User)
                .Include(c => c.ClaimFiles) //  include files
                .ToList();

            return View(claims);
        }

        [HttpGet("Home/MyClaims")]
        public IActionResult MyClaims()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
            {
                TempData["Error"] = "User not found. Please log in again.";
                return RedirectToAction("Index");
            }

            var claims = _context.Claims
                .Where(c => c.UserId == user.UserId)
                .ToList();

            return View(claims);
        }


        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View("Index");
            }

            HttpContext.Session.SetString("UserEmail", user.Email);

            if (user.Role == "Lecturer")
                return RedirectToAction("LecturerForm", "Home");
            else
                return RedirectToAction("CoordinatorClaims", "Home");
        }


    }
}



