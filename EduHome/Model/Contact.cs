using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class Contact :BaseEntity
    {
        [StringLength(100)]
        [Required]
        public string LocLink { get; set; }
        [StringLength(100)]
        [Required]
        public string Location { get; set; }
        [StringLength(100)]
        [Required]
        public string Adress { get; set; }
        [StringLength(100)]
        [Required]
        public string Phone { get; set; }
        [StringLength(100)]
       
        public string LocIcon { get; set; }
        [StringLength(100)]
      
        public string PhonIcon { get; set; }
        [StringLength(100)]
       
        public string AdressIcon { get; set; }



        [NotMapped]
     
        public IFormFile LocFile { get; set; }
        [NotMapped]
       
        public IFormFile PhonFile { get; set; }
        [NotMapped]
      
        public IFormFile AdressFile { get; set; }

    }
}
