using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Dal;
using mvc.Models;
using mvc.ModelView;
namespace mvc.Controllers
{
    public class FacultyAdminstratorController : Controller
    {
        // GET: FacultyAdminstrator
        public ActionResult HomePage()
        {


            return View();
        }


            public ActionResult ShowUsers()
        {
            UserDal userDal = new UserDal();
            UserViewModel userView = new UserViewModel();
            userView.users = new List<User>();
            foreach (User user in userDal.users)
            {
                userView.users.Add(user);
            }
            return View(userView.users);
        }

        public ActionResult AddCourse()
        {
            return View("AddCourse");
        }

        [HttpPost]
        public ActionResult Submit(Course course)
        {
            ScheduleDal dal = new ScheduleDal();
            LecturerDal lecturerDal = new LecturerDal();
                CourseDal courseDal = new CourseDal();
            if (!lecturerDal.IsFound(course.Lecturer))
            {
                ViewBag.error = "  the lecturer is not found";
                return View("AddCourse");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Session["shid"] = (dal.schedules.Count() + 1).ToString();
                    course.shid = Session["shid"].ToString();
                    courseDal.courses.Add(course);
                    courseDal.SaveChanges();
                    return RedirectToAction("addschule");
                    

                }
                catch (Exception)
                {
                        ViewBag.error1= "Enter another course number";
                        
                        return View("AddCourse");

                }

                    //throw;
                }
            
            else
            {

                // ViewData["error1"] = "Enter another course number";
                ViewBag.error1 = "Enter another course number";
            }
                return View("AddCourse");
                //if(ModelState.IsValid)
                //    return View(Content("HI EVERY THING IS OK BYBY"));
                //else
                //{
                //    return RedirectToAction("AddCourse");
                //}



                //return View("ShowUsers");
           // }
                
        }



        public ActionResult addStudent()
        {
            return View();
        }

        [HttpPost]

        public ActionResult add()
        {
            string username = Request.Form["username"];
            string cid = Request.Form["cid"];

            if (ModelState.IsValid)
            {
                CourseDal c = new CourseDal();
                Course course = c.courses.Find(cid);

                StudentDal s = new StudentDal();

                Student student = s.Dalstudents.Find(username);

                if (student == null)
                {
                    ModelState.AddModelError(string.Empty, "Student Name not exists.");
                    return View("addStudent");
                }
                if (course == null)
                {
                    ModelState.AddModelError(string.Empty, "Course cid not exists.");
                    return View("addStudent");

                }

                LearnDal learn = new LearnDal();
                Learn l = new Learn();
                l.cid = course.cid;
                l.Susername = student.username;
                l.shid = course.shid;
                learn.learns.Add(l);
                try
                {
                    learn.SaveChanges();
                }
                catch (Exception)
                {

                    ModelState.AddModelError(string.Empty, "this student already  not exists.");
                }




            }

            return View("Homepage");

        }

        public ActionResult editGrade()
        {
            return View();
        }

        public ActionResult update()
        {
            //CourseDal c = new CourseDal();
            // StudentDal st = new StudentDal();
            LearnDal l = new LearnDal();

            Grade g = new Grade();
            g.cid = Request.Form["cid"];
            g.username = Request.Form["username"];
            g.grade1 = Request.Form["grade"];


            string a1 = Request.Form["update"];

            if (a1 == null)
                a1 = "i";
            Gradedal dal = new Gradedal();
            List<Grade> s = dal.grade.ToList<Grade>();

            if (l.learns.Find(g.username,g.cid) == null)
            {

                ModelState.AddModelError(string.Empty, "Student Name not exists.");
                return View("editGrade");

            }

            



            if (a1.Equals("u"))
            {


                Grade a = dal.grade.Find(g.username, g.cid);
                if (a == null)
                {
                    ModelState.AddModelError(string.Empty, " this Student didnt have grade in this course to update.");
                    return View("editGrade");


                }

                a.grade1 = g.grade1;
                try
                {

                dal.SaveChanges();
                }
                catch (SqlException)
                {
                    ModelState.AddModelError(string.Empty, "The Student have grade in this course you can do update.");

                }

            }


            if (a1.Equals("i"))
            {
                dal.grade.Add(g);
                dal.SaveChanges();

            }



            return View("editGrade");

        }


        public ActionResult addschule()
        {

            return View(new Shedule());
        }


        public ActionResult addsc(Shedule s)
        {

            ScheduleDal dal = new ScheduleDal();

            s.shid = Session["shid"].ToString() ;//Session["shid"].ToString();

            if (ModelState.IsValid)
            {
                dal.schedules.Add(s);
                dal.SaveChanges();

                return View("Homepage");

            }
            else
                return View("addschule", s);

        }


        public ActionResult back()
        {
            return View("Homepage");

        }

    }
}