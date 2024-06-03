using AutoMapper;
using ballastLaneApi.Application.Interfaces;
using ballastLaneApi.Application.ViewModels;
using ballastLaneApi.Domain.Entities;
using ballastLaneApi.Infrastructure.Interfaces;


namespace ballastLaneApi.Application.Services;
public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<ProjectViewModel> CreateProject(ProjectViewModel project)
    {
        var projectEntity = _mapper.Map<Project>(project);
        var createdProject = await _projectRepository.CreateProject(projectEntity);
        return _mapper.Map<ProjectViewModel>(createdProject);
    }

    public async Task DeleteProject(int id)
    {
        var project = await _projectRepository.GetProject(id);
        if (project == null) throw new Exception("Project does not exist");
        await _projectRepository.DeleteProject(id);
    }

    public async Task<ProjectViewModel> GetProject(int id)
    {
        var project = await _projectRepository.GetProject(id);
        if (project == null) return null;
        return _mapper.Map<ProjectViewModel>(project);
    }

    public async Task<IEnumerable<ProjectViewModel>> GetProjects()
    {
        var projects = await _projectRepository.GetProjects();
        return _mapper.Map<IEnumerable<ProjectViewModel>>(projects);
    }

    public async Task<ProjectViewModel> UpdateProject(int id, ProjectViewModel project)
    {
        project.ProjectId = id;
        var projectEntity = _mapper.Map<Project>(project);
        var updatedProject = await _projectRepository.UpdateProject(projectEntity);
        return _mapper.Map<ProjectViewModel>(updatedProject);
    }
}