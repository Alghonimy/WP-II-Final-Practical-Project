using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UniversityApp.Models
{
    public class CrsResult
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Degree is required.")]
        [Range(0, 100, ErrorMessage = "Degree must be between 0 and 100.")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Degree { get; set; }
        [Required(ErrorMessage = "Course is required.")]
        [Display(Name = "Course")]
        public int CrsId { get; set; }
        [Required(ErrorMessage = "Trainee is required.")]
        [Display(Name = "Trainee")]
        public int TraineeId { get; set; }
        public Course? Course { get; set; }
        public Trainee? Trainee { get; set; }
    }
}