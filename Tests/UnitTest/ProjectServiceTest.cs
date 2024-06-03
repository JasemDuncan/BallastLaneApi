namespace ballastLaneApi.Tests.UnitTest.ProjectServiceTest;

using AutoMapper;
using ballastLaneApi.Application.Interfaces;
using ballastLaneApi.Application.Services;
using ballastLaneApi.Application.ViewModels;
using ballastLaneApi.Domain.Entities;
using ballastLaneApi.Infrastructure.Interfaces;
using ballastLaneApi.Mapping;
using Moq;
using NUnit.Framework;

public class ProjectServiceTest
{
    private Mock<IProjectRepository> _projectRepository;
    private IProjectService _projectService;
    private List<Project> _projects;
    private Project _project;
    private ProjectViewModel _projectViewModel;
    private IMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        _projectRepository = new Mock<IProjectRepository>();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
        _projectService = new ProjectService(_projectRepository.Object, _mapper);
        _projects = new List<Project>
        {
            new() {
                ProjectId = 1,
                Name = "Project 1",
                Description = "Description 1",
                Cost = 1000,
                Sales = 2000
            },
            new() {
                ProjectId = 2,
                Name = "Project 2",
                Description = "Description 2",
                Cost = 2000,
                Sales = 4000
            }
        };
        _project = new Project
        {
            ProjectId = 3,
            Name = "Project 3",
            Description = "Description 3",
            Cost = 3000,
            Sales = 6000
        };
        _projectViewModel = new ProjectViewModel
        {
            ProjectId = 3,
            Name = "Project 3",
            Description = "Description 3",
            Cost = 3000,
            Sales = 6000
        };
    }

    [Test]
    public async Task GetAllProjects()
    {
        // Arrange
        var projectsTemp = new List<Project>
        {
            new() {
                ProjectId = 1,
                Name = "Project 1",
                Description = "Description 1",
                Cost = 1000,
                Sales = 2000
            },
            new() {
                ProjectId = 2,
                Name = "Project 2",
                Description = "Description 2",
                Cost = 2000,
                Sales = 4000
            }
        };
        _projectRepository.Setup(p => p.GetProjects()).ReturnsAsync(projectsTemp);
        // Act
        var result = await _projectService.GetProjects();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetProjectById()
    {
        _projectRepository.Setup(p => p.GetProject(3)).ReturnsAsync(_project);
        var result = await _projectService.GetProject(3);
        Assert.That(result, Is.Not.Null);
        Assert.That(3, Is.EqualTo(result.ProjectId));
    }

    [Test]
    public async Task CreateProject()
    {
        _projectRepository.Setup(p => p.CreateProject(It.IsAny<Project>())).ReturnsAsync((Project p) => p);
        var result = await _projectService.CreateProject(_projectViewModel);
        Assert.That(result, Is.Not.Null);
        Assert.That(3, Is.EqualTo(result.ProjectId));
    }

    [Test]
    public async Task UpdateProject()
    {
        _projectRepository.Setup(p => p.UpdateProject(It.IsAny<Project>())).ReturnsAsync((Project p) => p);
        var result = await _projectService.UpdateProject(3, _projectViewModel);
        Assert.That(result, Is.Not.Null);
        Assert.That(3, Is.EqualTo(result.ProjectId));
    }

    [Test]

    public async Task DeleteProject()
    {
        _projectRepository.Setup(p => p.DeleteProject(3));
        _projectRepository.Setup(p => p.DeleteProject(3));
        await _projectService.DeleteProject(3);
        Assert.Pass();
    }

    [Test]
    public async Task GetProjectById_NotFound()
    {
        _projectRepository.Setup(p => p.GetProject(4)).ReturnsAsync((Project)null);
        var result = await _projectService.GetProject(4);
        Assert.That(result, Is.Null);
    }
}