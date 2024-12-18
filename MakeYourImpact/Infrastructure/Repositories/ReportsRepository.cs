using MakeYourImpact.Infrastructure.Repositories.Interfaces;
using MakeYourImpact.Models.Entities;
using MongoDB.Driver;

namespace MakeYourImpact.Infrastructure.Repositories;

public class ReportsRepository : IReportsRepository
{
    private readonly IMongoCollection<ReportEntity> _reportsCollection;

    public ReportsRepository(VolonteerDbContext dbContext)
    {
        _reportsCollection = dbContext.Reports;
    }

    public async Task<List<ReportEntity>> GetAllAsync()
    {
        return await _reportsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<ReportEntity?> GetByIdAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Report ID cannot be null or whitespace.", nameof(id));

        return await _reportsCollection.Find(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(ReportEntity report)
    {
        if (report == null)
            throw new ArgumentNullException(nameof(report), "Report cannot be null.");

        if (string.IsNullOrWhiteSpace(report.Location))
            throw new ArgumentException("Location cannot be null or empty.", nameof(report.Location));

        if (string.IsNullOrWhiteSpace(report.Description))
            throw new ArgumentException("Description cannot be null or empty.", nameof(report.Description));

        await _reportsCollection.InsertOneAsync(report);
    }

    public async Task UpdateAsync(ReportEntity report)
    {
        if (report == null)
            throw new ArgumentNullException(nameof(report), "Report cannot be null.");

        if (string.IsNullOrWhiteSpace(report.Id))
            throw new ArgumentException("Report ID cannot be null or empty.", nameof(report.Id));

        var filter = Builders<ReportEntity>.Filter.Eq(r => r.Id, report.Id);

        var result = await _reportsCollection.ReplaceOneAsync(filter, report);
        if (result.MatchedCount == 0)
            throw new KeyNotFoundException($"No report found with ID {report.Id}.");
    }

    public async Task DeleteAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Report ID cannot be null or empty.", nameof(id));

        var filter = Builders<ReportEntity>.Filter.Eq(r => r.Id, id);

        var result = await _reportsCollection.DeleteOneAsync(filter);
        if (result.DeletedCount == 0)
            throw new KeyNotFoundException($"No report found with ID {id}.");
    }
}