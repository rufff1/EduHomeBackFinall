using EduHome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels
{
    public class EventVM
    {
        public Event Event { get; set; }
        public IEnumerable<CourseCategory> CourseCategories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<EventTag> EventTags { get; set; }
        public Blog Blog { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
   
    }
}
