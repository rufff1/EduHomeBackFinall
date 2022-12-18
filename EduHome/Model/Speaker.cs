using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class Speaker : BaseEntity
    {
        [StringLength(100)]
        public string FullName { get; set; }
        [StringLength(100)]
        public string Image { get; set; }
        [StringLength(50)]
        public string Position { get; set; }


        public IEnumerable<EventSpeaker> EventSpeakers { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
