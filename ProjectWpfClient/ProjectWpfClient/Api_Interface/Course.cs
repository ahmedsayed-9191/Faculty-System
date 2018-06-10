using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWpfClient
{
    class Course
    {
        public int ID { get; set; }
        public int DoctorID { get; set; }
        public string Describtion { get; set; }
        public string Code { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
