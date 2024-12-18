using MakeYourImpact.Models.Entities;

namespace MakeYourImpact.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Provides methods for managing user applications in the database.
/// </summary>
public interface IUserApplicationsRepository
{
    /// <summary>
    /// Retrieves all user applications from the database.
    /// </summary>
    /// <returns>A list of all user applications.</returns>
    Task<List<UserApplicationEntity>> GetAllAsync();

    /// <summary>
    /// Retrieves a user application by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user application.</param>
    /// <returns>The user application with the specified ID, or null if not found.</returns>
    Task<UserApplicationEntity?> GetByIdAsync(string id);

    /// <summary>
    /// Adds a new user application to the database.
    /// </summary>
    /// <param name="userApplication">The user application to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(UserApplicationEntity userApplication);

    /// <summary>
    /// Updates an existing user application in the database.
    /// </summary>
    /// <param name="userApplication">The user application with updated information.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the user application with the specified ID does not exist.</exception>
    Task UpdateAsync(UserApplicationEntity userApplication);

    /// <summary>
    /// Deletes a user application from the database by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user application to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the user application with the specified ID does not exist.</exception>
    Task DeleteAsync(string id);
}