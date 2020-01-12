using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using mvc.Models;
namespace mvc.Dal
{
    public class Gradedal: DbContext
    {

        public DbSet<Grade> grade { get; set; }
    

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Grade>().ToTable("Grade");

            
        }
        

    }
}