using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsPart1
{
    internal class Student : Courses
    {
        public string Name { get; }
        public int Id { get; }
        public Student(string name, int id)
        {
            Name = name;
            Id = id;
        }
        
    }
}
