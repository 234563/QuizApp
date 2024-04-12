using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuizApp.Enities;
using QuizApp.Models;
using QuizApp.Services;
using System.Text.Json.Serialization;

namespace QuizApp.Controllers
{
    public class AuthController : Controller
    {
        /// <summary>
        /// Auth service to authenticate ueser
        /// </summary>
        public AuthServices AuthServices { get; set; }
        /// <summary>
        /// DbContext to save data to database 
        /// </summary>
        public ApplicationDbContext DbContext { get; set; }

        public AuthController(AuthServices authServices , ApplicationDbContext dbContext)
        {
            AuthServices = authServices;
            DbContext = dbContext;
        }


        [AllowAnonymous]
        public IActionResult Register(string returnurl)
        {
            ViewBag.ReturnUrl = returnurl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, string returnurl)
        {
            ModelState.Remove("returnUrl");
            if (ModelState.IsValid)
            {

                var result = await AuthServices.AuthenticateRegister(model);
                if (result.IsSucessfull)
                {

                    DbContext.ApplicationUsers.Add(new Enities.User() {Username =model.UserName,  Email = model.Email, Password = model.Password , DateOfBirth = model.DateOfBirth , Address = model.Address , PhoneNumber = model.Phone  });
                    await DbContext.SaveChangesAsync();

                    HttpContext.Session.SetString("useremail", model.Email);
                    HttpContext.Session.SetString("username",  model.UserName);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnurl)
        {
            ViewBag.ReturnUrl = returnurl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnurl)
        {
            ModelState.Remove("returnUrl");
            if (ModelState.IsValid)
            {

                var result = await AuthServices.Authenticate(model);
                if (result.IsSucessfull)
                {
                    var LoggedInUser = JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(result.Data));
                    HttpContext.Session.SetString("useremail" , LoggedInUser.Email);
                    HttpContext.Session.SetString("username", LoggedInUser.Username);
                    return RedirectToAction("Index", "Home");
                }
            }
            
            return View();
        }

        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();//.SetString("useremail", LoggedInUser.Email);
            return RedirectToAction("Index", "Home");
        }
    }
}
