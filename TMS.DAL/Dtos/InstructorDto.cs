using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TMS.DAL.Dtos
{
    public class InstructorDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the instructor's first name.")]
        [StringLength(50, ErrorMessage = "The first name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter the instructor's last name.")]
        [StringLength(50, ErrorMessage = "The last name cannot exceed 50 characters.")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Please enter the instructor's last name.")]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "The last name cannot exceed 50 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the instructor's last name.")]
        [StringLength(50, ErrorMessage = "The last name cannot exceed 50 characters.")]
        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RegisteredDate { get; set; }

        [Display(Name = "Available Courses")]
        public int? SelectedCourseId { get; set; }

        public IEnumerable<SelectListItem>? Courses { get; set; }

    }
}
