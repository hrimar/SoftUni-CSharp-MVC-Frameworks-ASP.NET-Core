using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoftUniClone.Models;

namespace SoftUniClone.Data
{

    public class SoftUniCloneDbContext : IdentityDbContext<User>
    {
        public SoftUniCloneDbContext(DbContextOptions<SoftUniCloneDbContext> options)
                : base(options)
        {
        }

       
        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseInstance> CourseInstances { get; set; }

        public DbSet<Lecture> Lectures { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<ResourceType> ResourceTypes { get; set; }

        public DbSet<StudentsInCourses> StudentsInCourses { get; set; }

        public DbSet<HomeworkSubmition> HomeworkSubmitions { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.EnrolledCourses)
                .WithOne(ec => ec.Student)
                .HasForeignKey(ec => ec.StudentId);

            builder.Entity<CourseInstance>()
               .HasMany(c=>c.Students)
               .WithOne(st=>st.Course)
               .HasForeignKey(st=>st.CourseId);

            builder.Entity<StudentsInCourses>()
                .HasKey(sc => new { sc.CourseId, sc.StudentId });

            builder.Entity<HomeworkSubmition>()
                .HasKey(hs => new { hs.AuthorId, hs.LectureId });

            //builder.Entity<IdentityRole>().HasData(new IdentityRole(), new IdentityRole());

            base.OnModelCreating(builder);
        }
    }
}
