using MakeYourImpact.Infrastructure.Repositories.Interfaces;
using MakeYourImpact.Models.Entities;
using MakeYourImpact.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourImpact.Controllers;

/// <summary>
/// Handles HTTP requests for managing reports.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportsRepository _reportsRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportsController"/> class.
    /// </summary>
    /// <param name="reportsRepository">The repository for managing reports.</param>
    public ReportsController(IReportsRepository reportsRepository)
    {
        _reportsRepository = reportsRepository;
    }

    /// <summary>
    /// Retrieves all reports.
    /// </summary>
    /// <returns>A list of all reports.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<ReportEntity>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var reports = await _reportsRepository.GetAllAsync();
        return Ok(reports);
    }

    /// <summary>
    /// Retrieves a report by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the report.</param>
    /// <returns>The report with the specified ID, or a 404 status code if not found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ReportEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("Report ID must not be null or empty.");

        var report = await _reportsRepository.GetByIdAsync(id);
        if (report == null)
            return NotFound($"Report with ID {id} not found.");

        return Ok(report);
    }

    /// <summary>
    /// Creates a new report.
    /// </summary>
    /// <param name="request">The report details to create.</param>
    /// <returns>The created report.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ReportEntity), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] ReportRequestModel request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var report = new ReportEntity
        {
            Date = request.Date,
            Location = request.Location,
            Description = request.Description,
            Results = request.Results
        };

        await _reportsRepository.AddAsync(report);
        return CreatedAtAction(nameof(GetById), new { id = report.Id }, report);
    }

    /// <summary>
    /// Updates an existing report by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the report to update.</param>
    /// <param name="request">The updated report details.</param>
    /// <returns>The updated report.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ReportEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(string id, [FromBody] ReportRequestModel request)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("Report ID must not be null or empty.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingReport = await _reportsRepository.GetByIdAsync(id);
        if (existingReport == null)
            return NotFound($"Report with ID {id} not found.");

        existingReport.Date = request.Date;
        existingReport.Location = request.Location;
        existingReport.Description = request.Description;
        existingReport.Results = request.Results;

        await _reportsRepository.UpdateAsync(existingReport);
        return Ok(existingReport);
    }

    /// <summary>
    /// Deletes a report by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the report to delete.</param>
    /// <returns>A 204 No Content response if successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("Report ID must not be null or empty.");

        var existingReport = await _reportsRepository.GetByIdAsync(id);
        if (existingReport == null)
            return NotFound($"Report with ID {id} not found.");

        await _reportsRepository.DeleteAsync(id);
        return NoContent();
    }
}