using Microsoft.AspNetCore.Mvc.Rendering;


namespace TMS.DAL.Dtos
{
    public class CareerPathDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
