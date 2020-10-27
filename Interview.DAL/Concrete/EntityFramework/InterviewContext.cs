using Interview.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.DAL.Concrete.EntityFramework
{
    public class InterviewContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-JC8BJ6K\\SQLEXPRESS;database=Interview;integrated security=true;");
        }

        public DbSet<Product> Products { get; set; }


    }
}
