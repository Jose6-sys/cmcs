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


        public IActionResult MyClaims(int userId)
        {
            var claims = _context.Claims
                .Include(c => c.ClaimFiles)
                .Where(c => c.UserId == userId)
                .ToList();

            ViewBag.UserId = userId;
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

            // Save to session
            HttpContext.Session.SetString("UserEmail", user.Email);

            // Redirect based on role
            if (user.Role == "Lecturer")
                return RedirectToAction("LecturerForm", "Home");

            return RedirectToAction("CoordinatorClaims", "Home");
        }

    }
}



