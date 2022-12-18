using EduHome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels
{
    public class BlogVM
    {
        public IEnumerable<CourseCategory> CourseCategories { get; set; }
        public IEnumerable<BlogTag> BlogTags { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public Blog Blog { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
    
    }
}
