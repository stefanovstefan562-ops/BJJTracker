using AutoMapper;
using BjjTracker.Models;
using BjjTracker.ViewModels;

namespace BjjTracker.Mapping
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile() 
        {
            CreateMap<Attendance, AttendanceViewModel>()
                .ForMember(d => d.AcademyName, o => o.MapFrom(s => s.TrainingSession != null && s.TrainingSession.Academy != null ? s.TrainingSession.Academy.AcademyName : null))
                .ForMember(d => d.PractitionerName, o => o.MapFrom(s => s.Practitioner != null ? s.Practitioner.FirstName + " " + s.Practitioner.LastName : null))
                .ForMember(d => d.DateOfTheTraining, o => o.MapFrom(s => s.TrainingSession!.DateOfTheTraining))
                .ForMember(d => d.StartTime, o => o.MapFrom(s => s.TrainingSession!.StartTime))
                .ForMember(d => d.DurationOfTheTraining, o => o.MapFrom(s => s.TrainingSession!.DurationOfTheTraining))
                .ForMember(d => d.TrainingType, o => o.MapFrom(s => s.TrainingSession!.TrainingType));

            CreateMap<AttendanceViewModel, Attendance>()
                .ForMember(d => d.TrainingSession, o => o.Ignore())
                .ForMember(d => d.Practitioner, o => o.Ignore());
        }
    }
}
