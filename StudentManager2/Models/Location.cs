using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace StudentManager2.Models
{
    public class Location
    {
        public int LocationID { get; set; }

        public string Building { get; set; }

        [Display(Name = "Room Number")]
        public double RoomNumber { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
        // In View: Html.EditorFor(m => m.Notes)

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}