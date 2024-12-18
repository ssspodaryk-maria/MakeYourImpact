using MakeYourImpact.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Core.Compression;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using MakeYourImpact.Models.Entities;

namespace MakeYourImpact.Infrastructure;

public class VolonteerDbContext
{
    private const string ProjectsCollectionName = "Projects";
    private const string UsersCollectionName = "Users";
    private const string UserApplicationsCollectionName = "UserApplications";
    private const string ReportsCollectionName = "Reports";

    private readonly IMongoDatabase _database;

    public VolonteerDbContext(IOptions<MongoDbConfig> dbOptions)
    {
        var mongoDbConfig = dbOptions.Value;

        var mongoUrl = new MongoUrlBuilder
        {
            Server = new MongoServerAddress(mongoDbConfig.Host),
            Username = string.IsNullOrEmpty(mongoDbConfig.User) ? null : mongoDbConfig.User,
            Password = string.IsNullOrEmpty(mongoDbConfig.Password) ? null : mongoDbConfig.Password,
            UseTls = mongoDbConfig.UseTls,
            Scheme = mongoDbConfig.UseSrv
                ? ConnectionStringScheme.MongoDBPlusSrv
                : ConnectionStringScheme.MongoDB,
            ApplicationName = string.IsNullOrWhiteSpace(mongoDbConfig.ApplicationName)
                ? "settings api"
                : mongoDbConfig.ApplicationName,
            MinConnectionPoolSize = mongoDbConfig.MinConnectionPoolSize <= 0
                ? MongoDefaults.MinConnectionPoolSize
                : mongoDbConfig.MinConnectionPoolSize,
            MaxConnectionPoolSize = mongoDbConfig.MaxConnectionPoolSize <= 0
                ? MongoDefaults.MaxConnectionPoolSize
                : mongoDbConfig.MaxConnectionPoolSize,
            MaxConnectionLifeTime = mongoDbConfig.MaxConnectionLifeTimeInMinutes <= 0
                ? MongoDefaults.MaxConnectionLifeTime
                : TimeSpan.FromMinutes(mongoDbConfig.MaxConnectionLifeTimeInMinutes),
            MaxConnecting = mongoDbConfig.MaxConnectingConcurrently <= 0
                ? 2
                : mongoDbConfig.MaxConnectingConcurrently,
            Compressors = new CompressorConfiguration[] { new(CompressorType.Zlib) },
            RetryWrites = true,
            W = WriteConcern.WMajority.W
        }.ToMongoUrl();

        var client = new MongoClient(mongoUrl);
        _database = client.GetDatabase(mongoDbConfig.Database);
    }

    public IMongoCollection<ProjectEntity> Projects => _database.GetCollection<ProjectEntity>(ProjectsCollectionName);
    public IMongoCollection<UserEntity> Users=> _database.GetCollection<UserEntity>(UsersCollectionName);

    public IMongoCollection<UserApplicationEntity> UserApplications => _database.GetCollection<UserApplicationEntity>(UserApplicationsCollectionName);
    public IMongoCollection<ReportEntity> Reports => _database.GetCollection<ReportEntity>(ReportsCollectionName);
}
