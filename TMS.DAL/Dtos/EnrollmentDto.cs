using TMS.DAL.Enums;

namespace TMS.DAL.Dtos
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TraineeId { get; set; }
        public string TraineeName { get; set; }
        public Grade? Grade { get; set; }
    }
}
