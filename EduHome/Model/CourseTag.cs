using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class CourseTag : BaseEntity
    {
              //MANY TO MANY 

        public Course Course{ get; set; }
        public int CourseId { get; set; }

             
        public Tag Tag { get; set; }
        public int TagId { get; set; }

    }
}
