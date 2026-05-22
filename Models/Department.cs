using System.ComponentModel.DataAnnotations;

namespace UniversityApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Department Name")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Manager name is required.")]
        [StringLength(100, ErrorMessage = "Manager name cannot exceed 100 characters.")]
        [Display(Name = "Manager")]
        public string Manager { get; set; } = string.Empty;
        public List<Instructor> Instructors { get; set; } = new();
        public List<Trainee> Trainees { get; set; } = new();
        public List<Course> Courses { get; set; } = new();
    }
}