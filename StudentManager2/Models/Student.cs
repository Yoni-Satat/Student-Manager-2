using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManager2.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date), Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Matriculation No.")]
        public string MatricNumber { get; set; }

        public string Gender { get; set; }
        public bool Adjustments { get; set; }
        public string Origin { get; set; }

        [Display(Name = "Year of study")]
        public int YearOfStudy { get; set; }

        [Display(Name = "Image URL")]
        public string ImageURL { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }


        public virtual ICollection<StudyGroup> StudyGroups { get; set; }
        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; }
    }
}