using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class CourseCategory : BaseEntity
    {

        [StringLength(100)]
        [Required]
        public string Name { get; set; }


        public List<Course> Courses { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Event> Events { get; set; }
    }
}
