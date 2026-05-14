using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorExcelApp.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        
        [Required]
        public string ProjectName { get; set; } = string.Empty;
        
        [Required]
        public string TaskName { get; set; } = string.Empty;
        
        public string AssignedTo { get; set; } = string.Empty;
        
        public DateTime? StartDate { get; set; }
        public int DaysRequired { get; set; }
        public DateTime? EndDate { get; set; }
        public double Progress { get; set; }
    }
}
