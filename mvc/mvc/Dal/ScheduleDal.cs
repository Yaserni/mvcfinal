using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using mvc.Models;
namespace mvc.Dal
{
    public class ScheduleDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Shedule>().ToTable("Shedule");
            
        }
        public DbSet<Shedule> schedules { get; set; }
    }
}