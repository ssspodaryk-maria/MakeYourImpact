using MakeYourImpact.Models.Entities;

namespace MakeYourImpact.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Provides methods for managing projects in the database.
/// </summary>
public interface IProjectsRepository
{
    /// <summary>
    /// Retrieves all projects from the database.
    /// </summary>
    /// <returns>A list of all projects.</returns>
    Task<List<ProjectEntity>> GetAllAsync();

    /// <summary>
    /// Retrieves a project by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the project.</param>
    /// <returns>The project with the specified ID, or null if not found.</returns>
    Task<ProjectEntity?> GetByIdAsync(string id);

    /// <summary>
    /// Adds a new project to the database.
    /// </summary>
    /// <param name="project">The project to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAsync(ProjectEntity project);

    /// <summary>
    /// Updates an existing project in the database.
    /// </summary>
    /// <param name="project">The project with updated information.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the project with the specified ID does not exist.</exception>
    Task UpdateAsync(ProjectEntity project);

    /// <summary>
    /// Deletes a project from the database by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the project to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the project with the specified ID does not exist.</exception>
    Task DeleteAsync(string id);
}
