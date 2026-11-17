using AutoMapper;
using BjjTracker.Models;
using BjjTracker.ViewModels;

namespace BjjTracker.Mapping
{
    public class TechniqueProfile : Profile
    {
        public TechniqueProfile()
        {
            CreateMap<Technique, TechniqueViewModel>().ReverseMap();
        }
    }
}
