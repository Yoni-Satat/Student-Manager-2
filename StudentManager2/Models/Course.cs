using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using StudentManager2.Extentions;

namespace StudentManager2.Models
{
    public class Course
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        [Display(Name = "Course Title")]
        public string Title { get; set; }
        [Display(Name = "Course Level")]
        public string Level { get; set; }

        public virtual ICollection<StudyGroup> StudyGroups { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }

        
    }
}