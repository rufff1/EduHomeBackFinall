using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.ViewModels
{
    public class ProfileVM
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string CurrentPaswoord { get; set; }

        [DataType(DataType.Password)]
        public string NewPaswoord { get; set; }

        [Compare(nameof(NewPaswoord))]
        [DataType(DataType.Password)]
        public string ConfirmPaswoord { get; set; }
    }
}
