using EduHome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels
{
    public class AboutVM 
    {

        public WelcomeCourse  WelcomeCourse { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public Testimional testimional { get; set; }
        public IEnumerable<NoticeBoard> NoticeBoards { get; set; }
        public IEnumerable<NoticeRight> NoticeRights { get; set; }
    }
}
