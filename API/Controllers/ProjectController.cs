using ballastLaneApi.Application.Interfaces;
using ballastLaneApi.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ballastLaneApi.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectViewModel>>> GetProjects()
    {
        var projects = await _projectService.GetProjects();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectViewModel>> GetProject(int id)
    {
        var project = await _projectService.GetProject(id);
        return Ok(project);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectViewModel>> CreateProject(ProjectViewModel project)
    {
        var newProject = await _projectService.CreateProject(project);
        return Ok(newProject);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectViewModel>> UpdateProject(int id, ProjectViewModel project)
    {
        var updatedProject = await _projectService.UpdateProject(id, project);
        return Ok(updatedProject);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProject(int id)
    {
        await _projectService.DeleteProject(id);
        return Ok();
    }
}