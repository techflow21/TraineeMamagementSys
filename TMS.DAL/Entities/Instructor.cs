
namespace TMS.DAL.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisteredDate { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
