using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PracticeForTest.DbCtx;
using PracticeForTest.Models;

namespace PracticeForTest.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        // Instantiate MukeshDb with a specific initial catalog
        MukeshDb db = new MukeshDb();

        [AllowAnonymous]
        public ActionResult Register()
        {
            List<string> city = new List<string>()
            {
                "Noida",
                "Delhi",
                "Mumbai",
                "Bhubaneswar"
            };
            SelectList list = new SelectList(city);
            ViewBag.City = list;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(UserModel r)
        {
            if (ModelState.IsValid)
            {
                List<string> city = new List<string>()
            {
                "Noida",
                "Delhi",
                "Mumbai",
                "Bhubaneswar"
            };
                SelectList list = new SelectList(city);
                ViewBag.City = list;

                var existingUser = db.Users.Where(f => f.username == r.username).FirstOrDefault();
                if (existingUser != null)
                {
                    ModelState.AddModelError("username", "Username is already taken.");
                    return View(r);
                }
                existingUser = db.Users.Where(f => f.email == r.email).FirstOrDefault();
                if (existingUser != null)
                {
                    ModelState.AddModelError("email", "Email is already taken.");
                    return View(r);
                }
                if (r.password != r.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");
                    return View(r);
                }

                User newUser = new User();
                newUser.username = r.username;
                newUser.email = r.email;
                newUser.password = r.password;
                newUser.fullname = r.fullname;
                newUser.userId = r.userId;
                newUser.city = r.city;
                newUser.dateOfBirth = r.dateOfBirth;
                newUser.gender = r.gender;

                db.Users.Add(newUser);
                db.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(r);
        }
        // GET: Auth
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login (UserModel l)
        {
            var res = db.Users.Where(f => f.username == l.username).FirstOrDefault();
            if (res == null)
            {
                ModelState.AddModelError("username", "Please Enter Valid Username");
            }
            else
            {
                if(res.username == l.username && res.password == l.password)
                {
                    FormsAuthentication.SetAuthCookie(res.username, false);
                    Session["username"] = res.username;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("password", "Please Enter Valid Password");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}