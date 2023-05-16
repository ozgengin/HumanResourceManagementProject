using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class HumanResourceContextSeed
    {
        public static async Task SeedAsync(HumanResourceContext db)
        {
            await db.Database.MigrateAsync();

            if (await db.Professions.AnyAsync() || await db.Departments.AnyAsync())
                return;

            var d1 = new Department() { Name = "Software Development" };
            var d2 = new Department() { Name = "Project Management" };
            var d3 = new Department() { Name = "Testing" };
            var d4 = new Department() { Name = "Data Science" };
            var d5 = new Department() { Name = "Business Analysis" };
            var d6 = new Department() { Name = "Information Security" };

            db.Professions.AddRange(
                new Profession() { Department = d1, Name = "Software Developer" },
                new Profession() { Department = d1, Name = "Software Engineer" },
                new Profession() { Department = d1, Name = "Full-Stack Developer" },
                new Profession() { Department = d1, Name = "Front-End Developer" },
                new Profession() { Department = d1, Name = "Back-End Developer" },
                new Profession() { Department = d2, Name = "Project Manager" },
                new Profession() { Department = d2, Name = "Project Coordinator" },
                new Profession() { Department = d2, Name = "Project Analyst" },
                new Profession() { Department = d3, Name = "Test Engineer" },
                new Profession() { Department = d3, Name = "Test Automation Engineer" },
                new Profession() { Department = d3, Name = "System Test Engineer" },
                new Profession() { Department = d3, Name = "Application Test Engineer" },
                new Profession() { Department = d4, Name = "Data Scientist" },
                new Profession() { Department = d4, Name = "Data Engineer" },
                new Profession() { Department = d4, Name = "Machine Learning Specialist" },
                new Profession() { Department = d4, Name = "Artificial Intelligence Expert" },
                new Profession() { Department = d5, Name = "Business Analyst" },
                new Profession() { Department = d5, Name = "System Analyst" },
                new Profession() { Department = d5, Name = "Data Analyst" },
                new Profession() { Department = d6, Name = "Information Security Specialist" },
                new Profession() { Department = d6, Name = "Security Analyst" }
               );
            await db.SaveChangesAsync();
        }
    }
}
