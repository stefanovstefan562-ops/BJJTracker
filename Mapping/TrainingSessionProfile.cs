using AutoMapper;
using BjjTracker.Models;
using BjjTracker.ViewModels;

namespace BjjTracker.Mapping
{
    public class TrainingSessionProfile : Profile
    {
        public TrainingSessionProfile() 
        {
            CreateMap<TrainingSession, TrainingSessionViewModel>()
            .ForMember(d => d.AcademyName, o => o.MapFrom(s => s.Academy != null ? s.Academy.AcademyName : null))
            .ReverseMap();
        }
    }
}
