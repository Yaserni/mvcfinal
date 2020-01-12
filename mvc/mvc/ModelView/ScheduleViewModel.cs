using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mvc.Models;

namespace mvc.ModelView
{
    public class ScheduleViewModel
    {
        public Shedule schedule { get; set; }
        public Learn Learn { get; set; }
        public Course course{ get; set; }
        public List<Shedule> schedules { get; set; }
        public List<Learn> learns { get; set; }
        public List<Course> courses { get; set; }
    }
}