using System.ComponentModel.DataAnnotations;
namespace UniversityApp.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(255)]
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(255)]
        public string Address { get; set; } = string.Empty;
        [Required(ErrorMessage = "Grade is required.")]
        [StringLength(10)]
        public string Grade { get; set; } = string.Empty;
        [Required(ErrorMessage = "Department is required.")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<CrsResult> CrsResults { get; set; } = new();
    }
}