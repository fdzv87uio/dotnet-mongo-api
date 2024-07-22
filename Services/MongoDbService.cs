using DotnetMongoApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DotnetMongoApi.Services;

public class MongoDbService {

    private readonly IMongoCollection<Movie> _moviesCollection;

    public MongoDbService (IOptions<MongoDbSettings> mongoDbSettings){
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _moviesCollection = database.GetCollection<Movie>(mongoDbSettings.Value.CollectionName);
    }

    public async Task CreateAsync(Movie movie){
        _moviesCollection.InsertOneAsync(movie);
        return;
    }

    public async Task<List<Movie>> GetAsync(){
        return await _moviesCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddActorToMovie (string id, string actorName){
        FilterDefinition<Movie> filter = Builders<Movie>.Filter.Eq("Id", id);
        UpdateDefinition<Movie> update = Builders<Movie>.Update.AddToSet<string>("cast", actorName);
        await _moviesCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteMovie (string id){
        FilterDefinition<Movie> filter = Builders<Movie>.Filter.Eq("Id", id);
        await _moviesCollection.DeleteOneAsync(filter);
        return;
    }
}