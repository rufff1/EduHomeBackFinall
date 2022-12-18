using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class Skill : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }
        public IEnumerable<TeacherSkill> TeacherSkills { get; set; }
    }
}
