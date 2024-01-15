using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/games")
            .WithParameterValidation();


        // Get requests

        group.MapGet("/", (IGamesRepository repository) =>
            repository.GetAll().Select(game => game.AsDto()));

        group.MapGet("/{id}", (IGamesRepository repository, int id) =>
            {
                Game? game = repository.Get(id);
                return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
            }
        )
        .WithName(GetGameEndpointName);

        // Post requests

        group.MapPost("/", (IGamesRepository repository, CreateGameDto gameDto) =>
            {
                Game game = new()
                {
                    Name = gameDto.Name,
                    Genre = gameDto.Genre,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    ImageUri = gameDto.ImageUri
                };

                repository.Create(game);
                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.ID }, game);
            }
        );


        // Put requests

        group.MapPut("/{id}", (IGamesRepository repository, int id, UpdateGameDto UpdatedGameDto) =>
            {


                Game? existingGame = repository.Get(id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                existingGame.Name = UpdatedGameDto.Name;
                existingGame.Genre = UpdatedGameDto.Genre;
                existingGame.Price = UpdatedGameDto.Price;
                existingGame.ReleaseDate = UpdatedGameDto.ReleaseDate;
                existingGame.ImageUri = UpdatedGameDto.ImageUri;

                repository.Update(existingGame);

                return Results.NoContent();
            }
        );


        // Patch requests

        group.MapPatch("/{id}", (IGamesRepository repository, int id, UpdateGameDto UpdatedGameDto) =>
            {
                Game? existingGame = repository.Get(id);

                if (existingGame is null)
                {
                    Game game = new()
                    {
                        Name = UpdatedGameDto.Name,
                        Genre = UpdatedGameDto.Genre,
                        Price = UpdatedGameDto.Price,
                        ReleaseDate = UpdatedGameDto.ReleaseDate,
                        ImageUri = UpdatedGameDto.ImageUri
                    };

                    repository.Create(game);
                    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.ID }, game);
                }

                existingGame.Name = UpdatedGameDto.Name;
                existingGame.Genre = UpdatedGameDto.Genre;
                existingGame.Price = UpdatedGameDto.Price;
                existingGame.ReleaseDate = UpdatedGameDto.ReleaseDate;
                existingGame.ImageUri = UpdatedGameDto.ImageUri;

                repository.Update(existingGame);

                return Results.NoContent();
            }
        );


        // Delete requests

        group.MapDelete("/{id}", (IGamesRepository repository, int id) =>
            {
                Game? existingGame = repository.Get(id);

                if (existingGame is not null)
                {
                    repository.Delete(id);
                }

                return Results.NoContent();
            }
        );

        return group;
    }
}