using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirst_LaptrinhWeb.Models;
using BCrypt;
namespace CodeFirst_LaptrinhWeb.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Products");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (user != null)
            {
                MyDBContext db = new MyDBContext();
                User myUser = db.users.Where(u => u.UserName == user.UserName).FirstOrDefault();
                if (myUser != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(user.Password, myUser.Password))
                    {
                        HttpCookie authCookie = new HttpCookie("auth", myUser.UserName);
                        HttpCookie roleCookie = new HttpCookie("role", myUser.Role);
                        Response.Cookies.Add(authCookie);
                        Response.Cookies.Add(roleCookie);
                        if (myUser.Role == "Admin")
                        {
                            return RedirectToAction("Index", "Manage",new { area="Admin"});
                        }
                        else
                        {
                            return RedirectToAction("Index", "Products");
                        }
                  
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid username or password.");
                }
               
            }
            return View();
        }
        public ActionResult Logout()
        {
            HttpCookie authCookie = new HttpCookie("auth");
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpCookie roleCookie = new HttpCookie("role");
            roleCookie.Expires = DateTime.Now.AddDays(-1);

            Response.Cookies.Add(authCookie);
            Response.Cookies.Add(roleCookie);

            
            return RedirectToAction("Index", "Products");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user, string retypePassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (user.Password != retypePassword)
            {
                ModelState.AddModelError("retypePassword", "Passwords do not match.");
                return View();
            }

            MyDBContext db = new MyDBContext();
            User myUser = db.users.Where(u => u.UserName == user.UserName).FirstOrDefault();
            if (myUser != null)
            {
                ModelState.AddModelError("UserName", "Username already exist.");
                return View();
            }
            myUser = db.users.Where(u => u.EmailAddress == user.EmailAddress).FirstOrDefault();
            if (myUser != null)
            {
                ModelState.AddModelError("EmailAddress", "EmailAddress already exist.");
                return View();
            }

            myUser = new User();
            myUser.UserName = user.UserName;
            myUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            myUser.EmailAddress = user.EmailAddress;
            myUser.Role = "user";
            db.users.Add(myUser);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}