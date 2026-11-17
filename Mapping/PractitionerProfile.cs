using AutoMapper;
using BjjTracker.Models;
using BjjTracker.ViewModels;

namespace BjjTracker.Mapping
{
    public class PractitionerProfile : Profile
    {
        public PractitionerProfile()
        {
            CreateMap<Practitioner, PractitionerViewModel>()
                .ForMember(d => d.AcademyName, o => o.MapFrom(s => s.Academy != null ? s.Academy.AcademyName : null))
                .ReverseMap();
        }
    }
}
