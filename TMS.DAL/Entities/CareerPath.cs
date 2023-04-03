
using System.ComponentModel.DataAnnotations;

namespace TMS.DAL.Entities
{
    public class CareerPath
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
