
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using TMS.DAL.Dtos;
using TMS.DAL.Entities;

namespace TMS.BLL.MappingProfile
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CareerPathDto, CareerPath>();
            CreateMap<CareerPath, CareerPathDto>();

            CreateMap<CourseDto, Course>();
            CreateMap<Course, CourseDto>();

            CreateMap<InstructorDto, Instructor>();
            CreateMap<Instructor, InstructorDto>();

            CreateMap<TraineeDto, Trainee>();
            CreateMap<Trainee, TraineeDto>();


            CreateMap<SelectListItem, Course>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => int.Parse(src.Value)))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Text));

        }

    }
}
