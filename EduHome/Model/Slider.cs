﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class Slider :BaseEntity
    {
        [StringLength(150)]
      
        public string Image { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(150)]
        public string link { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
