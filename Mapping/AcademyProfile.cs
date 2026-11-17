using AutoMapper;
using BjjTracker.Models;
using BjjTracker.ViewModels;

namespace BjjTracker.Mapping
{
    public class AcademyProfile : Profile
    {
        public AcademyProfile()
        {
            CreateMap<Academy, AcademyViewModel>().ReverseMap();
        }
    }
}
