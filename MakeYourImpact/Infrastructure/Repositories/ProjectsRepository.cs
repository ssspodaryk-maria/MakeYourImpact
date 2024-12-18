using MakeYourImpact.Infrastructure.Repositories.Interfaces;
using MakeYourImpact.Models.Entities;
using MongoDB.Driver;

namespace MakeYourImpact.Infrastructure.Repositories;

public class ProjectsRepository : IProjectsRepository
{
    private readonly IMongoCollection<ProjectEntity> _projectsCollection;

    public ProjectsRepository(VolonteerDbContext dbContext)
    {
        _projectsCollection = dbContext.Projects ?? throw new ArgumentNullException(nameof(dbContext.Projects), "Projects collection is null.");
    }

    public async Task<List<ProjectEntity>> GetAllAsync()
    {
        return await _projectsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<ProjectEntity?> GetByIdAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Project ID cannot be empty.", nameof(id));

        return await _projectsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(ProjectEntity project)
    {
        if (project == null)
            throw new ArgumentNullException(nameof(project), "Project cannot be null.");

        if (string.IsNullOrWhiteSpace(project.Title))
            throw new ArgumentException("Project title cannot be empty or null.", nameof(project.Title));

        if (string.IsNullOrWhiteSpace(project.Location))
            throw new ArgumentException("Project location cannot be empty or null.", nameof(project.Location));

        if (project.VolCount < 1)
            throw new ArgumentException("Volunteer count must be at least 1.", nameof(project.VolCount));

        await _projectsCollection.InsertOneAsync(project);
    }

    public async Task UpdateAsync(ProjectEntity project)
    {
        if (project == null)
            throw new ArgumentNullException(nameof(project), "Project cannot be null.");

        if (string.IsNullOrWhiteSpace(project.Id))
            throw new ArgumentException("Project ID cannot be null or whitespace.", nameof(project.Id));

        var filter = Builders<ProjectEntity>.Filter.Eq(p => p.Id, project.Id);

        var result = await _projectsCollection.ReplaceOneAsync(filter, project);
        if (result.MatchedCount == 0)
            throw new KeyNotFoundException($"No project found with ID {project.Id}.");
    }

    public async Task DeleteAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Project ID cannot be null or whitespace.", nameof(id));

        var filter = Builders<ProjectEntity>.Filter.Eq(p => p.Id, id);

        var result = await _projectsCollection.DeleteOneAsync(filter);
        if (result.DeletedCount == 0)
            throw new KeyNotFoundException($"No project found with ID {id}.");
    }
}
