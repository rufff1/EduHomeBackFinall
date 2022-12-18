using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class Tag : BaseEntity
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public List<CourseTag> CourseTags { get; set; }
        public IEnumerable<EventTag> EventTags { get; set; }
        public IEnumerable<BlogTag> BlogTags { get; set; }
    }
}
