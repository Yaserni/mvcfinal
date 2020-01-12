using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Dal;
using mvc.ModelView;
using mvc.Models;

namespace mvc.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult showschedule()
        {
            ScheduleDal sheduleDal = new ScheduleDal();
            LearnDal learnDal = new LearnDal();
            CourseDal courseDal = new CourseDal();
          
            ScheduleViewModel scheduleViewModel = new ScheduleViewModel();
            scheduleViewModel.learns = new List<Learn>();
            foreach (Learn learn in learnDal.learns.ToList<Learn>())
            {
                if (learn.Susername.Equals( Session["username"]))
                    scheduleViewModel.learns.Add(learn);
               
            }
            List<string> shids = new List<string>();
            List<string> cids = new List<string>();

            foreach (Learn learn in scheduleViewModel.learns)
            {
                shids.Add(learn.shid);
                cids.Add(learn.cid);
            }
            //scheduleViewModel.schedules = sheduleDal.schedules.ToList<Shedule>();
            scheduleViewModel.schedules = new List<Shedule>();
            foreach (Shedule shedule in sheduleDal.schedules.ToList<Shedule>())
            {
                if( shids.Contains(shedule.shid))
                    scheduleViewModel.schedules.Add(shedule);

            }
            scheduleViewModel.courses = new List<Course>();
            foreach (Course course in courseDal.courses.ToList<Course>())
            {
                if (cids.Contains(course.cid))
                    scheduleViewModel.courses.Add(course);
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

        public ActionResult StudentHomePage()
        {
            return View();

        }

        public ActionResult ShowStudent()
        {
            StudentDal std = new StudentDal();
            StudentViewModel studentViewModel = new StudentViewModel();
            studentViewModel.students = std.Dalstudents.ToList<Student>();

            return View(studentViewModel);

        }

        public ActionResult ShowExams()
        {
            LearnDal learnDal = new LearnDal();
            CourseDal courseDal = new CourseDal();
            Session["username"] = "las";
            ScheduleViewModel scheduleViewModel = new ScheduleViewModel();
            scheduleViewModel.learns = new List<Learn>();
            foreach (Learn learn in learnDal.learns.ToList<Learn>())
            {
                if (learn.Susername.Equals(Session["username"]))
                    scheduleViewModel.learns.Add(learn);

            }
            List<string> cids = new List<string>();

            foreach (Learn learn in scheduleViewModel.learns)
            {
                if(!cids.Contains(learn.cid))
                    cids.Add(learn.cid);
            }

            //scheduleViewModel.schedules = sheduleDal.schedules.ToList<Shedule>();
            scheduleViewModel.courses = new List<Course>();
            foreach (Course course in courseDal.courses.ToList<Course>())
            {
                if (cids.Contains(course.cid))
                    scheduleViewModel.courses.Add(course);
            }


            return View(scheduleViewModel.courses);
        }

        //username cid grade 

        public ActionResult ShowGrade()
        {
            Gradedal gradedal = new Gradedal();
            return View(gradedal.grade);


        }

        
    }




}