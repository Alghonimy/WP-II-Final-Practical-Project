using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityApp.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [StringLength(255)]
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(255)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required.")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Display(Name = "Course")]
        public int? CrsId { get; set; }

        // Navigation properties
        public Department? Department { get; set; }
        public Course? Course { get; set; }
    }
}