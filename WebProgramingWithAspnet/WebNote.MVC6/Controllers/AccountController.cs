using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebNote.MVC6.DataContext;
using WebNote.MVC6.Models;
using WebNote.MVC6.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebNote.MVC6.Controllers
{
    public class AccountController : Controller
    {
        // GET: /<controller>/

        /// <summary>
        /// Sign In
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            return RedirectToAction("Error", "Account");

            // ID, PW authentication
            if (ModelState.IsValid)
            {
                using (var db = new WebNoteDbContext())
                {
                    // LinQ - method chaining
                    //var user = db.Users.FirstOrDefault(c => 
                    //c.Id == model.Id && c.Password == model.Password);     <- model.Id wrapping! memory rick
                    var user = db.Users.FirstOrDefault(c =>
                        c.Id.Equals(model.UserId) && c.Password.Equals(model.UserPassword));

                    if (user != null)
                    {
                        // Login Success
                        HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.No);
                        return RedirectToAction("LoginSuccess", "Home");
                    }
                }

                // Login Fail
                ModelState.AddModelError(string.Empty, "Invalid ID or Password");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("USER_LOGIN_KEY");

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Sign Up
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }


        /// <summary>
        /// Sign Up post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(User model)
        {
            return RedirectToAction("Error", "Account");

            if (ModelState.IsValid)
            {
                using (var db = new WebNoteDbContext())     // using keywork : Disposiable work [ ex) try ~ finally { Dispose() } ]
                {
                    db.Users.Add(model);    // ride on memory
                    db.SaveChanges();       // real database commit
                }
                return RedirectToAction("Index", "Home");   // refresh other page
            }

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
