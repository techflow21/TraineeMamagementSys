using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace TMS.DAL.Dtos
{
    public class CourseDto
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }


        [StringLength(500)]
        public string? Description { get; set; }

        [Display(Name = "Career Path")]
        public int? CareerPathId { get; set; }

        public CareerPathDto? CareerPath { get; set; }

        public IEnumerable<SelectListItem>? CareerPaths { get; set; }
    }
}
