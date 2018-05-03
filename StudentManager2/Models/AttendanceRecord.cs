using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManager2.Models
{
    public class AttendanceRecord
    {
        public int AttendanceRecordID { get; set; }
        public int? StudyGroupID { get; set; }
        public int? LocationID { get; set; }
        public int? CourseID { get; set; }
        public int? LessonID { get; set; }
        [Display(Name = "Tutor Name")]
        public string TutorName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public virtual Course Course { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual StudyGroup StudyGroup { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        
    }
}