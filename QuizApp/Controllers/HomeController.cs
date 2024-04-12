using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Models;
using System.Diagnostics;

namespace QuizApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext DbContext;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext applicationDb)
        {
            _logger = logger;
            DbContext = applicationDb;
        }

        public IActionResult Index()
        {
            ///Get Logged In user 
            var loggedinuser =  HttpContext.Session.GetString("username");
            ViewBag.LoggedInUser = loggedinuser;

            /// Load User 
            var userList = DbContext.ApplicationUsers.OrderByDescending(u => u.TotalWins).ToList();

            return View(userList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
