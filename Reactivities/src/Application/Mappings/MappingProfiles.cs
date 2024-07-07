using Application.Commands;
using Application.Dtos;
using AutoMapper;

namespace Application.Mappings;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Activity, ActivityDto>();
        CreateMap<CreateActivityDto, CreateActivityCommand>();
        CreateMap<CreateActivityCommand, Activity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<ActivityDto, EditActivityCommand>();
        CreateMap<EditActivityCommand, Activity>();
    }
}
