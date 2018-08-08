using System;
using System.ComponentModel.DataAnnotations;

namespace MyTT.Web.Models {
    public class PlanItem {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public bool IsDone { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueAt { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan Time { get; set; }
    }
}