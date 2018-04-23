using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManager2.Models
{
    public class StudyGroup
    {
        

        public int StudyGroupID { get; set; }
        public int CourseID { get; set; }

        [Display(Name = "Group Title")]
        public string GroupTitle { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}

