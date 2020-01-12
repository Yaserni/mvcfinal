using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using mvc.Models;
namespace mvc.Dal
{
public class LecturerDal: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Lecturer>().ToTable("Lecturer");
        }
        public DbSet<Lecturer> lecturers{ get; set; }

        public bool IsFound(String Lecturer)
        {
            foreach (Lecturer obj in lecturers)
            {
                if (Lecturer!=null && Lecturer.Equals(obj.username))
                    return true;
            }
            return false;
        }
    }
}
 