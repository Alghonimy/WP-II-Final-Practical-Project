using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UniversityApp.Models
{  public class Course
    {   public int Id { get; set; }
        [Required(ErrorMessage = "Course name is required.")]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Degree is required.")]
        [Range(0, 100, ErrorMessage = "Degree must be between 0 and 100.")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Degree { get; set; }
        [Required(ErrorMessage = "Minimum degree is required.")]
        [Display(Name = "Min Degree")]
        [Range(0, 100, ErrorMessage = "Min degree must be between 0 and 100.")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal MinDegree { get; set; }
        [Required(ErrorMessage = "Hours are required.")]
        [Range(1, 20, ErrorMessage = "Hours must be between 1 and 20.")]
        [Display(Name = "Credit Hours")]
        public int Hrs { get; set; }
        [Required(ErrorMessage = "Department is required.")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<Instructor> Instructors { get; set; } = new();
        public List<CrsResult> CrsResults { get; set; } = new();}}