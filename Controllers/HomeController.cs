using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginReg.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace LoginReg.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(MainUser newUser)
        {
            if(ModelState.IsValid)
            {
                // two ways to check for unique Email - FirstOrDefault() or .Any()
                // var userCheck = dbContext.Users.FirstOrDefault(u => u.Email == newUser.Email);
                // if(userCheck != null)
                // {
                //     ModelState.AddModelError("Email", "This Email is already in use!");
                // }

                if(dbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "This Email is already in use!");
                    return View("Register");
                }
                // hash password before sending to db!!
                //passwordhasher of MainUser type
                PasswordHasher<MainUser> hasher = new PasswordHasher<MainUser>();
                string hashedPw = hasher.HashPassword(newUser, newUser.Password);
                // update user model with hashedpassword
                newUser.Password = hashedPw; 

                //add newUser to DB
                var newUserAdded = dbContext.Users.Add(newUser).Entity;
                dbContext.SaveChanges();

                HttpContext.Session.SetInt32("userId", newUserAdded.UserId);

                return RedirectToAction("Success");
            }
            // if form not valid return to registration page
            return View("Register");
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            if(HttpContext.Session.GetInt32("userId")== null)
            {
                return RedirectToAction("Register");
            }
            return View();
        }

        [HttpGet("logout")]
        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Register");
        }
    }
}
