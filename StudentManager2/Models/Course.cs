using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManager2.Models
{
    public class Course
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Level { get; set; }

        public virtual ICollection<StudyGroup> StudyGroups { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}