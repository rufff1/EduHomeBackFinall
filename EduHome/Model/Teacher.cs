using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class Teacher :BaseEntity
    {

        [StringLength(50)]
        [Required]
        public string FullName { get; set; }
        [StringLength(300)]
        public string Image { get; set; }
        [StringLength(90)]
        [Required]
        public string Hobby { get; set; }
        [Required]
        [StringLength(1000)]
        public string About { get; set; }
        [StringLength(90)]
        [Required]
        public string Degree { get; set; }
        [StringLength(90)]
        [Required]
        public string Experience { get; set; }
        [StringLength(90)]
        [Required]
        public string Faculty { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(90)]
        public string Email { get; set; }
        [StringLength(90)]
        public string Phone { get; set; }
        [StringLength(90)]
        public string Skype { get; set; }

        public string Fblink { get; set; }
        public string PinttLink { get; set; }
        public string VimeoLink { get; set; }
        public string TwtLink { get; set; }


        public int TeacherPositionId { get; set; }

        public TeacherPosition TeacherPosition { get; set; }
        public IEnumerable<TeacherSkill> TeacherSkills { get; set; }


        //admin panel

        [NotMapped]
        [MaxLength(4)]
        public IEnumerable<int> SkillIds { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }


    }
}
