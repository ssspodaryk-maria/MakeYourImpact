namespace MakeYourImpact.Config;

public record MongoDbConfig
{
    public static string ConfigurationSection { get; private set; } = "Mongo";

    public string Host { get; init; } = string.Empty;

    public string User { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public bool UseTls { get; init; }

    public bool UseSrv { get; init; }

    public string ApplicationName { get; init; } = string.Empty;

    public int MinConnectionPoolSize { get; init; }

    public int MaxConnectionPoolSize { get; init; }

    public int MaxConnectionLifeTimeInMinutes { get; init; }

    public int MaxConnectingConcurrently { get; init; }
    public string Database { get; init; } = "VolunteerDB";
}
