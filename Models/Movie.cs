using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace DotnetMongoApi.Models;

public class Movie {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string title { get; set; } = null!;

    public string plot { get; set; } = null!;

    [BsonElement("cast")]
    [JsonPropertyName("cast")]
    public List<string> cast { get; set; } = null!;

}