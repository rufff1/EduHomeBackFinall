using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class Event : BaseEntity
    {
        [StringLength(200)] 
        public string Image { get; set; }
        [StringLength(220)]
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime MonthDay { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public string Venue { get; set; }
        [StringLength(2000)]
        [Required]
        public string Description { get; set; }


        public IEnumerable<EventSpeaker> EventSpeakers { get; set; }

        public IEnumerable<EventTag> EventTags { get; set; }

        public int? CourseCategoryId { get; set; }
        public CourseCategory CourseCategory { get; set; }



        [NotMapped]
        [MaxLength(3)]
        public IEnumerable<int> TagIds { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        [MaxLength(3)]
        public IEnumerable<int> SpeakerIds { get; set; }

    }
}
