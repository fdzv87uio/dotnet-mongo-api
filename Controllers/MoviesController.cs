using System;
using Microsoft.AspNetCore.Mvc;
using DotnetMongoApi.Models;
using DotnetMongoApi.Services;
using MongoDB.Driver;

namespace DotnetMongoApi.Controllers;

[Controller]
[Route("api/[controller]")]
public class MoviesController : Controller {

    private readonly MongoDbService _mongoDbService;

    public MoviesController(MongoDbService mongoDbService){
        _mongoDbService = mongoDbService;
    }

    [HttpGet]
    public async Task<List<Movie>> Get(){
        return await _mongoDbService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Movie movie){
        await _mongoDbService.CreateAsync(movie);
        return CreatedAtAction(nameof(Get), new {id = movie.Id}, movie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AddActor(string id, [FromBody] string actorName){
        await _mongoDbService.AddActorToMovie(id, actorName);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id){
        await _mongoDbService.DeleteMovie(id);
        return NoContent();
    }


}