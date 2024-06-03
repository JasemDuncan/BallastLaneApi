using ballastLaneApi.Domain.Entities;

namespace ballastLaneApi.Infrastructure.Interfaces;
public interface IProjectRepository
{
    Task<Project> CreateProject(Project project);
    Task DeleteProject(int id);
    Task<Project> GetProject(int id);
    Task<IEnumerable<Project>> GetProjects();
    Task<Project> UpdateProject(Project project);
}