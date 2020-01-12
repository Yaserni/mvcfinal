using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using mvc.Models;
namespace mvc.Dal
{
    public class LearnDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Learn>().ToTable("Learn");

        }
        public DbSet<Learn> learns { get; set; }
    }
}
