
namespace TMS.DAL.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime RegisteredDate { get; set; } = DateTime.Now;

    }
}
