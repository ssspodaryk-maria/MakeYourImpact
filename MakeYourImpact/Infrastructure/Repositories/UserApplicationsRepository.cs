using MakeYourImpact.Infrastructure.Repositories.Interfaces;
using MakeYourImpact.Models.Entities;
using MongoDB.Driver;

namespace MakeYourImpact.Infrastructure.Repositories;

/// <summary>
/// Implementation of <see cref="IUserApplicationsRepository"/> for managing user applications in MongoDB.
/// </summary>
public class UserApplicationsRepository : IUserApplicationsRepository
{
    private readonly IMongoCollection<UserApplicationEntity> _userApplicationsCollection;

    public UserApplicationsRepository(VolonteerDbContext dbContext)
    {
        _userApplicationsCollection = dbContext.UserApplications;
    }

    public async Task<List<UserApplicationEntity>> GetAllAsync()
    {
        return await _userApplicationsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<UserApplicationEntity?> GetByIdAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("User Application ID cannot be null or whitespace.", nameof(id));

        return await _userApplicationsCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(UserApplicationEntity userApplication)
    {
        if (userApplication == null)
            throw new ArgumentNullException(nameof(userApplication), "User Application cannot be null.");

        if (string.IsNullOrWhiteSpace(userApplication.Title))
            throw new ArgumentException("Title cannot be null or empty.", nameof(userApplication.Title));

        if (string.IsNullOrWhiteSpace(userApplication.Description))
            throw new ArgumentException("Description cannot be null or empty.", nameof(userApplication.Description));

        await _userApplicationsCollection.InsertOneAsync(userApplication);
    }

    public async Task UpdateAsync(UserApplicationEntity userApplication)
    {
        if (userApplication == null)
            throw new ArgumentNullException(nameof(userApplication), "User Application cannot be null.");

        if (string.IsNullOrWhiteSpace(userApplication.Id))
            throw new ArgumentException("User Application ID cannot be null or empty.", nameof(userApplication.Id));

        var filter = Builders<UserApplicationEntity>.Filter.Eq(u => u.Id, userApplication.Id);

        var result = await _userApplicationsCollection.ReplaceOneAsync(filter, userApplication);
        if (result.MatchedCount == 0)
            throw new KeyNotFoundException($"No user application found with ID {userApplication.Id}.");
    }

    public async Task DeleteAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("User Application ID cannot be null or empty.", nameof(id));

        var filter = Builders<UserApplicationEntity>.Filter.Eq(u => u.Id, id);

        var result = await _userApplicationsCollection.DeleteOneAsync(filter);
        if (result.DeletedCount == 0)
            throw new KeyNotFoundException($"No user application found with ID {id}.");
    }
}