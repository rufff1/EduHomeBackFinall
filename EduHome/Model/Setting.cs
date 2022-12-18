﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}
