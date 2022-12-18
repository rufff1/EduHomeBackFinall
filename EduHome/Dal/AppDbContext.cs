using EduHome.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Dal
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherPosition> TeacherPositions { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }
        public DbSet<WelcomeCourse> WelcomeCourses { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<NoticeRight> NoticeRights { get; set; }
        public DbSet<WhyEduHome> WhyEduHomes { get; set; }
        public DbSet<Testimional> Testimionals { get; set; }
        public DbSet<EventTag> EventTags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<TeacherSkill> TeacherSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}
