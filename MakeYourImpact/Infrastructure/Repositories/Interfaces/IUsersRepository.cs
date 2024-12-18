using MakeYourImpact.Models.Entities;

namespace MakeYourImpact.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Provides methods for managing users in the database.
/// </summary>
public interface IUsersRepository
{
    /// <summary>
    /// Retrieves all users from the database.
    /// </summary>
    /// <returns>A list of all users.</returns>
    Task<List<UserEntity>> GetAllAsync();

    /// <summary>
    /// Retrieves a user by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>The user with the specified ID, or null if not found.</returns>
    Task<UserEntity?> GetByIdAsync(string id);

    /// <summary>
    /// Adds a new user to the database.
    /// </summary>
    /// <param name="user">The user to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(UserEntity user);

    /// <summary>
    /// Updates an existing user in the database.
    /// </summary>
    /// <param name="user">The user with updated information.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the user with the specified ID does not exist.</exception>
    Task UpdateAsync(UserEntity user);

    /// <summary>
    /// Deletes a user from the database by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the user with the specified ID does not exist.</exception>
    Task DeleteAsync(string id);
}