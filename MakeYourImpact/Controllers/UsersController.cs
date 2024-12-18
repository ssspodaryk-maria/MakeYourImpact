using MakeYourImpact.Infrastructure.Repositories.Interfaces;
using MakeYourImpact.Models.Entities;
using MakeYourImpact.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourImpact.Controllers;

/// <summary>
/// Handles HTTP requests for managing users.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _usersRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersController"/> class.
    /// </summary>
    /// <param name="usersRepository">The repository for managing users.</param>
    public UsersController(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A list of all users.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<UserEntity>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var users = await _usersRepository.GetAllAsync();
        return Ok(users);
    }

    /// <summary>
    /// Retrieves a user by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>The user with the specified ID, or a 404 status code if not found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("User ID must not be null or empty.");

        var user = await _usersRepository.GetByIdAsync(id);
        if (user == null)
            return NotFound($"User with ID {id} not found.");

        return Ok(user);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="request">The user details to create.</param>
    /// <returns>The created user.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(UserEntity), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] UserRequestModel request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new UserEntity
        {
            Name = request.Name,
            MobNumber = request.MobNumber,
            Email = request.Email,
            Age = request.Age,
            Bio = request.Bio,
            Location = request.Location,
            Role = request.Role
        };

        await _usersRepository.AddAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    /// <summary>
    /// Updates an existing user by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="request">The updated user details.</param>
    /// <returns>The updated user.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(string id, [FromBody] UserRequestModel request)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("User ID must not be null or empty.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _usersRepository.GetByIdAsync(id);
        if (existingUser == null)
            return NotFound($"User with ID {id} not found.");

        existingUser.Name = request.Name;
        existingUser.MobNumber = request.MobNumber;
        existingUser.Email = request.Email;
        existingUser.Age = request.Age;
        existingUser.Bio = request.Bio;
        existingUser.Location = request.Location;
        existingUser.Role = request.Role;

        await _usersRepository.UpdateAsync(existingUser);
        return Ok(existingUser);
    }

    /// <summary>
    /// Deletes a user by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    /// <returns>A 204 No Content response if successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("User ID must not be null or empty.");

        var existingUser = await _usersRepository.GetByIdAsync(id);
        if (existingUser == null)
            return NotFound($"User with ID {id} not found.");

        await _usersRepository.DeleteAsync(id);
        return NoContent();
    }
}