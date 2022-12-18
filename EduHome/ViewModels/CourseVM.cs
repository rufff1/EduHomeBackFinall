using EduHome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels
{
    public class CourseVM
    {

        public IEnumerable<Course> Courses { get; set; }
        public Course Course { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<CourseCategory> CourseCategories { get; set; }
        public IEnumerable<CourseTag> CourseTags { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
