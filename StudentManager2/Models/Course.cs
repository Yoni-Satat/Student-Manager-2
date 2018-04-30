﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using StudentManager2.DAL;

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

        [Display(Name = "Number of Lessons")]
        public int NumberOfLessons
        {
            get
            {
                var counter = 0;
                foreach (var l in Lessons)
                {
                    counter++;
                }
                return counter;
            }
        }
    }
}