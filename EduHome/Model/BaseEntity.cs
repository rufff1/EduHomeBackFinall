using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Model
{
    public class BaseEntity
    {

        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> CreatAt { get; set; }
        public string CreatBy { get; set; }
        public Nullable<DateTime> UpdateAt { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public string DeletedBy { get; set; }
    }
}
