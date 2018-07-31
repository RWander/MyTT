using System;
using System.ComponentModel.DataAnnotations;

namespace MyTT.Web.Models {
    public class PlanItem {
        public Guid Id { get; set; }

        public bool IsDone { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTimeOffset DueAt { get; set; }
    }
}