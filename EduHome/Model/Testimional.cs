using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class Testimional : BaseEntity
    {
        public string Image { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }


        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
