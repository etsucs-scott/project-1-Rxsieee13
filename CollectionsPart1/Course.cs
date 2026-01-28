using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsPart1
{
    internal class Course
    {
        public string Professor { get; }
        public string Title { get; }
        public string description { get; }
        public int Capacity { get; set; } = 30;
        public Student[] Roster;
    }
}
