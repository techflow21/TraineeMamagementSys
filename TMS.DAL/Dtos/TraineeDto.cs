using System.ComponentModel.DataAnnotations;


namespace TMS.DAL.Dtos
{
    public class TraineeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Last Name field is required.")]
        [StringLength(50, ErrorMessage = "The Last Name field cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The First Name field is required.")]
        [StringLength(50, ErrorMessage = "The First Name field cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Address field is required.")]
        [StringLength(500, ErrorMessage = "The Address field cannot exceed 500 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The Phone Number field is required.")]
        [StringLength(20, ErrorMessage = "The Phone Number field cannot exceed 20 characters.")]
        public string PhoneNumber { get; set; }
    }
}
