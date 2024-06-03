using ballastLaneApi.Application.ViewModels;

namespace ballastLaneApi.Application.Interfaces;
public interface IProjectService
{
    Task<IEnumerable<ProjectViewModel>> GetProjects();
    Task<ProjectViewModel> GetProject(int id);
    Task<ProjectViewModel> CreateProject(ProjectViewModel project);
    Task<ProjectViewModel> UpdateProject(int id, ProjectViewModel project);
    Task DeleteProject(int id);
}