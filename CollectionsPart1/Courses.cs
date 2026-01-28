using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsPart1
{
    internal class Courses : IEnumerable<Courses>
    {
        private readonly List<Courses> _courses = new List<Courses>();
        public void AddCourse(Courses course)
        {
            _courses.Add(course);
        }
        public bool RemoveCourse(Courses course)
        {
            return _courses.Remove(course);
        }
        public IEnumerator<Courses> GetEnumerator()
        {
            return _courses.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _courses.GetEnumerator();
        }
        
        foreach (var course in courses)
        {
            Console.WriteLine(course.Title);
    }
}
