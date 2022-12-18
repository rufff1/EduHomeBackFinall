using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class TeacherPosition :BaseEntity
    {
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public IEnumerable<Teacher> Teachers { get; set; }
    }
}
