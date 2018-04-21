using StudentManager2.Models;
using System.Collections.Generic;


namespace StudentManager2.ViewModels
{
    public class GroupIndexData
    {
        public IEnumerable<StudyGroup> StudyGroups { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
        public Course Course { get; set; }
    }
}