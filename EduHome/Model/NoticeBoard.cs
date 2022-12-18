using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class NoticeBoard : BaseEntity
    {
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }
        [StringLength(2000)]
        [Required]
        public string Description { get; set; }
        public string VideoLink { get; set; }
    }
}
