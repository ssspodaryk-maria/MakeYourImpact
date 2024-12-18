using MakeYourImpact.Infrastructure.Repositories.Interfaces;
using MakeYourImpact.Models.Entities;
using MakeYourImpact.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourImpact.Controllers;

/// <summary>
/// Handles HTTP requests for managing user applications.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserApplicationsController : ControllerBase
{
    private readonly IUserApplicationsRepository _userApplicationsRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserApplicationsController"/> class.
    /// </summary>
    /// <param name="userApplicationsRepository">The repository for managing user applications.</param>
    public UserApplicationsController(IUserApplicationsRepository userApplicationsRepository)
    {
        _userApplicationsRepository = userApplicationsRepository;
    }

    /// <summary>
    /// Retrieves all user applications.
    /// </summary>
    /// <returns>A list of all user applications.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<UserApplicationEntity>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var userApplications = await _userApplicationsRepository.GetAllAsync();
        return Ok(userApplications);
    }

    /// <summary>
    /// Retrieves a user application by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user application.</param>
    /// <returns>The user application with the specified ID, or a 404 status code if not found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserApplicationEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("User Application ID must not be null or empty.");

        var userApplication = await _userApplicationsRepository.GetByIdAsync(id);
        if (userApplication == null)
            return NotFound($"User Application with ID {id} not found.");

        return Ok(userApplication);
    }

    /// <summary>
    /// Creates a new user application.
    /// </summary>
    /// <param name="request">The user application details to create.</param>
    /// <returns>The created user application.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(UserApplicationEntity), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] UserApplicationRequestModel request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userApplication = new UserApplicationEntity
        {
            Title = request.Title,
            Description = request.Description,
            Status = request.Status
        };

        await _userApplicationsRepository.AddAsync(userApplication);
        return CreatedAtAction(nameof(GetById), new { id = userApplication.Id }, userApplication);
    }

    /// <summary>
    /// Updates an existing user application by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user application to update.</param>
    /// <param name="request">The updated user application details.</param>
    /// <returns>The updated user application.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserApplicationEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(string id, [FromBody] UserApplicationRequestModel request)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("User Application ID must not be null or empty.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingApplication = await _userApplicationsRepository.GetByIdAsync(id);
        if (existingApplication == null)
            return NotFound($"User Application with ID {id} not found.");

        existingApplication.Title = request.Title;
        existingApplication.Description = request.Description;
        existingApplication.Status = request.Status;

        await _userApplicationsRepository.UpdateAsync(existingApplication);
        return Ok(existingApplication);
    }

    /// <summary>
    /// Deletes a user application by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user application to delete.</param>
    /// <returns>A 204 No Content response if successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("User Application ID must not be null or empty.");

        var existingApplication = await _userApplicationsRepository.GetByIdAsync(id);
        if (existingApplication == null)
            return NotFound($"User Application with ID {id} not found.");

        await _userApplicationsRepository.DeleteAsync(id);
        return NoContent();
    }
}