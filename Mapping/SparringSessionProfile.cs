using AutoMapper;
using BjjTracker.Models;
using BjjTracker.ViewModels;

namespace BjjTracker.Mapping
{
    public class SparringSessionProfile : Profile
    {
        public SparringSessionProfile()
        {
            CreateMap<SparringSession, SparringSessionViewModel>()
                 .ForMember(d => d.TrainingDate, o => o.MapFrom(s => s.TrainingSession != null ? s.TrainingSession.DateOfTheTraining : (DateOnly?)null));

            CreateMap<SparringSessionViewModel, SparringSession>()
                .ForMember(d => d.TrainingSession, o => o.Ignore())
                .ForMember(d => d.Practitioner, o => o.Ignore());
        }
    }
}
