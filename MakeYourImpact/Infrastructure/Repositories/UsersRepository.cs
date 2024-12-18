using MakeYourImpact.Infrastructure.Repositories.Interfaces;
using MakeYourImpact.Models.Entities;
using MongoDB.Driver;

namespace MakeYourImpact.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly IMongoCollection<UserEntity> _usersCollection;

    public UsersRepository(VolonteerDbContext dbContext)
    {
        _usersCollection = dbContext.Users;
    }

    public async Task<List<UserEntity>> GetAllAsync()
    {
        return await _usersCollection.Find(_ => true).ToListAsync();
    }

    public async Task<UserEntity?> GetByIdAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("User ID cannot be null or whitespace.", nameof(id));

        return await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAsync(UserEntity user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user), "User cannot be null.");

        if (string.IsNullOrWhiteSpace(user.Name))
            throw new ArgumentException("User name cannot be null or empty.", nameof(user.Name));

        if (string.IsNullOrWhiteSpace(user.Email))
            throw new ArgumentException("User email cannot be null or empty.", nameof(user.Email));

        await _usersCollection.InsertOneAsync(user);
    }

    public async Task UpdateAsync(UserEntity user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user), "User cannot be null.");

        if (string.IsNullOrWhiteSpace(user.Id))
            throw new ArgumentException("User ID cannot be null or empty.", nameof(user.Id));

        var filter = Builders<UserEntity>.Filter.Eq(u => u.Id, user.Id);

        var result = await _usersCollection.ReplaceOneAsync(filter, user);
        if (result.MatchedCount == 0)
            throw new KeyNotFoundException($"No user found with ID {user.Id}.");
    }

    public async Task DeleteAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("User ID cannot be null or empty.", nameof(id));

        var filter = Builders<UserEntity>.Filter.Eq(u => u.Id, id);

        var result = await _usersCollection.DeleteOneAsync(filter);
        if (result.DeletedCount == 0)
            throw new KeyNotFoundException($"No user found with ID {id}.");
    }
}
