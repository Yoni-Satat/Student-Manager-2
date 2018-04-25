using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManager2.ViewModels
{
    public class AddStudentToGroup
    {
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public bool AddStudent { get; set; }
    }
}