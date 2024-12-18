using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace MakeYourImpact.Models.Entities;

public abstract class EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    [JsonIgnore]
    [JsonPropertyOrder(-1)]
    public string? Id { get; internal set; } = default!;// Унікальний ідентифікатор для всіх сутностей
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Час створення
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow; // Час останнього оновлення
}
