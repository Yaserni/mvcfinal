using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;
using mvc.ModelView;
using mvc.Dal;
namespace mvc.Controllers
{
    public class LecturerController : Controller
    {
        // GET: Lecturer
        public ActionResult ShowSchedule()
        {
           // Session["username"] = "alex";
            ScheduleDal sheduleDal = new ScheduleDal();
            LearnDal learnDal = new LearnDal();
            CourseDal courseDal = new CourseDal();
            ScheduleViewModel scheduleViewModel = new ScheduleViewModel();
            scheduleViewModel.learns = new List<Learn>();
            scheduleViewModel.courses = new List<Course>();
            
            List<string> shids = new List<string>();
            List<string> cids = new List<string>();

            foreach (Course course in courseDal.courses.ToList<Course>())
            {
                if (course.Lecturer.Equals(Session["username"].ToString()))
                {
                    cids.Add(course.cid);
                    scheduleViewModel.courses.Add(course);
                }
            }



            foreach (Learn learn in learnDal.learns.ToList<Learn>())
            {
                if (cids.Contains(learn.cid))
                {
                    shids.Add(learn.shid);
                    scheduleViewModel.learns.Add(learn);
                }
            }

            scheduleViewModel.schedules = new List<Shedule>();
            foreach (Shedule shedule in sheduleDal.schedules.ToList<Shedule>())
            {
                if (shids.Contains(shedule.shid))
                    scheduleViewModel.schedules.Add(shedule);

            }
            List<ScheduleViewModel> models = new List<ScheduleViewModel>();
            ScheduleViewModel model;
            for (int i = 0; i < scheduleViewModel.courses.Count && i < scheduleViewModel.learns.Count && i < scheduleViewModel.schedules.Count; i++)
            {
                model = new ScheduleViewModel();
                model.schedule = scheduleViewModel.schedules.ElementAt(i);
                model.Learn = scheduleViewModel.learns.ElementAt(i);
                model.course = scheduleViewModel.courses.ElementAt(i);
                models.Add(model);
            }
            return View(models);

        }
        public ActionResult ShowStudents(string cid)
        {
           // cid = "1";
            //string cid = "1";
            LearnDal learnDal = new LearnDal();
            StudentDal studentDal = new StudentDal();
            StudentViewModel viewModel = new StudentViewModel();
            List<string> usernames = new List<string>();
            foreach( Learn learn in learnDal.learns)
            {
                if (learn.cid.Equals(cid)) 
                {
                    usernames.Add(learn.Susername);
                }
            }

            viewModel.students = new List<Student>();
            foreach (Student student in studentDal.Dalstudents)
            {
                if (usernames.Contains(student.username))
                {
                    viewModel.students.Add(student);
                }

            }
            return View(  viewModel.students);
        }

        public ActionResult editgrade()
        {

            return View();

        }
        public ActionResult update()
        {

            CourseDal c = new CourseDal();
            StudentDal st = new StudentDal();


            Grade g = new Grade();
            g.cid = Request.Form["cid"];
            g.username = Request.Form["username"];
            g.grade1 = Request.Form["grade"];


            string a1 = Request.Form["update"];

            Gradedal dal = new Gradedal();
            List<Grade> s = dal.grade.ToList<Grade>();

            if (st.Dalstudents.Find(g.username) == null)
            {

                ModelState.AddModelError(string.Empty, "Student Name not exists.");
                return View("editgrade");

            }

            if (c.courses.Find(g.cid) == null)
            {
                ModelState.AddModelError(string.Empty, "Student Name not exists.");
                return View("editgrade");
            }



            if (a1.Equals("u"))
            {


                Grade a = dal.grade.Find(g.username, g.cid);
                if (a == null)
                {
                    ModelState.AddModelError(string.Empty, " this Student didnt have grade to update.");
                    return View("editgrade");


                }

                a.grade1 = g.grade1;

                dal.SaveChanges();

            }


            if (a1.Equals("i"))
            {
                dal.grade.Add(g);
                dal.SaveChanges();

            }



            return View("editgrade");

        }


        public ActionResult LecturerHomePage()
        {

            return View();
        }
    }



}