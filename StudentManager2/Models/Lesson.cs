using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using StudentManager2.Extentions;

namespace StudentManager2.Models
{
    public class Lesson
    {
        public int LessonID { get; set; }
        public int CourseID { get; set; }
        public int? LocationID { get; set; }

        public string Topic { get; set; }

        [Display(Name = "Schedule to start"), DataType(DataType.Time)]
        public DateTime? LessonStart { get; set; }

        [Display(Name = "Schedule to end"), DataType(DataType.Time)]
        public DateTime? LessonEnd { get; set; }

        [Display(Name = "Mark Mandatory"), DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        public bool IsMandatory { get; set; }
        

        public virtual Course Course { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; }
        
    }
}