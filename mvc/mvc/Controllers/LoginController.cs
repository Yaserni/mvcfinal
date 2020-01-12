using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Dal;
using mvc.Models;
using mvc.Controllers;
namespace mvc.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View("Login", new User());
        }

        [HttpPost]
        public ActionResult sumbit(User loge)
        {


            UserDal dal = new UserDal();

            List<User> logins = dal.users.ToList<User>();


            if (ModelState.IsValid)
            {

                foreach (User l in logins)
                {
                    if (l.username.Equals(loge.username) && l.password.Equals(loge.password))
                    {
                        if (l.type.Equals("0"))
                        {
                            Session["username"] = l.username;
                            return RedirectToAction("StudentHomePage", "Student");
                            

                        }
                        if (l.type.Equals("1"))
                        {
                            Session["username"] = l.username;
                            return RedirectToAction("LecturerHomePage", "Lecturer");
                        }
                        // home page lect
                        if (l.type.Equals("2"))
                        {
                            Session["username"] = l.username;
                            return RedirectToAction("Homepage", "FacultyAdminstrator");
                        }
                        //fac home page
                    }


                }

                ModelState.AddModelError(string.Empty, "Student Name not exists.");
            }



            return View("Login", loge);





        }


    }
}
