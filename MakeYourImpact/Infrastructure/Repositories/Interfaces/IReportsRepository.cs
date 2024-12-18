using MakeYourImpact.Models.Entities;

namespace MakeYourImpact.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Provides methods for managing reports in the database.
/// </summary>
public interface IReportsRepository
{
    /// <summary>
    /// Retrieves all reports from the database.
    /// </summary>
    /// <returns>A list of all reports.</returns>
    Task<List<ReportEntity>> GetAllAsync();

    /// <summary>
    /// Retrieves a report by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the report.</param>
    /// <returns>The report with the specified ID, or null if not found.</returns>
    Task<ReportEntity?> GetByIdAsync(string id);

    /// <summary>
    /// Adds a new report to the database.
    /// </summary>
    /// <param name="report">The report to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(ReportEntity report);

    /// <summary>
    /// Updates an existing report in the database.
    /// </summary>
    /// <param name="report">The report with updated information.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the report with the specified ID does not exist.</exception>
    Task UpdateAsync(ReportEntity report);

    /// <summary>
    /// Deletes a report from the database by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the report to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if the report with the specified ID does not exist.</exception>
    Task DeleteAsync(string id);
}
