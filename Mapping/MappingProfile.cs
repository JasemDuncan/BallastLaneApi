using AutoMapper;
using ballastLaneApi.Application.ViewModels;
using ballastLaneApi.Domain.Entities;

namespace ballastLaneApi.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserViewModel, User>();
        CreateMap<User, UserViewModel>();

        CreateMap<Project, ProjectViewModel>();
        CreateMap<ProjectViewModel, Project>();
    }
}
