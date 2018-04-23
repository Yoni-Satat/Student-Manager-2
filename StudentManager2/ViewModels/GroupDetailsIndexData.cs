using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentManager2.Models;

namespace StudentManager2.ViewModels
{
    public class GroupDetailsIndexData
    {
        public StudyGroup StudyGroup { get; set; }
        public Course Course { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}