using MakeYourImpact.Infrastructure.Repositories.Interfaces;
using MakeYourImpact.Models.Entities;
using MakeYourImpact.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourImpact.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsRepository _projectsRepository;

    public ProjectsController(IProjectsRepository projectsRepository)
    {
        _projectsRepository = projectsRepository;
    }

    /// <summary>
    /// Retrieves all projects.
    /// </summary>
    /// <returns>A list of all projects.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _projectsRepository.GetAllAsync();
        return Ok(projects);
    }

    /// <summary>
    /// Retrieves a project by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the project.</param>
    /// <returns>The project, or a 404 response if not found.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("Project ID must not be null or empty.");

        var project = await _projectsRepository.GetByIdAsync(id);
        if (project == null)
            return NotFound($"Project with ID {id} not found.");

        return Ok(project);
    }

    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="request">The project details.</param>
    /// <returns>The created project.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectRequestModel request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var project = new ProjectEntity
        {
            Title = request.Title,
            Date = request.Date,
            Location = request.Location,
            VolCount = request.VolCount,
            Description = request.Description,
            InitiatorFullName = request.InitiatorFullName
        };

        await _projectsRepository.AddAsync(project);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    /// <summary>
    /// Updates an existing project.
    /// </summary>
    /// <param name="id">The unique identifier of the project to update.</param>
    /// <param name="request">The updated project details.</param>
    /// <returns>The updated project.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] ProjectRequestModel request)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("Project ID must not be null or empty.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingProject = await _projectsRepository.GetByIdAsync(id);
        if (existingProject == null)
            return NotFound($"Project with ID {id} not found.");

        existingProject.Title = request.Title;
        existingProject.Date = request.Date;
        existingProject.Location = request.Location;
        existingProject.VolCount = request.VolCount;
        existingProject.Description = request.Description;
        existingProject.InitiatorFullName = request.InitiatorFullName;

        await _projectsRepository.UpdateAsync(existingProject);
        return Ok(existingProject);
    }

    /// <summary>
    /// Deletes a project by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the project to delete.</param>
    /// <returns>A 204 No Content response if successful.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("Project ID must not be null or empty.");

        var existingProject = await _projectsRepository.GetByIdAsync(id);
        if (existingProject == null)
            return NotFound($"Project with ID {id} not found.");

        await _projectsRepository.DeleteAsync(id);
        return NoContent();
    }
}
