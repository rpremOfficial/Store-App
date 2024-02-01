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

        group.MapGet("/", async (IGamesRepository repository) =>
            (await repository.GetAllAsync()).Select(game => game.AsDto()));

        group.MapGet("/{id}", async (IGamesRepository repository, int id) =>
            {
                Game? game = await repository.GetAsync(id);
                return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
            }
        )
        .WithName(GetGameEndpointName);

        // Post requests

        group.MapPost("/", async (IGamesRepository repository, CreateGameDto gameDto) =>
            {
                Game game = new()
                {
                    Name = gameDto.Name,
                    Genre = gameDto.Genre,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    ImageUri = gameDto.ImageUri
                };

                await repository.CreateAsync(game);
                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.ID }, game);
            }
        );


        // Put requests

        group.MapPut("/{id}", async (IGamesRepository repository, int id, UpdateGameDto UpdatedGameDto) =>
            {


                Game? existingGame = await repository.GetAsync(id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                existingGame.Name = UpdatedGameDto.Name;
                existingGame.Genre = UpdatedGameDto.Genre;
                existingGame.Price = UpdatedGameDto.Price;
                existingGame.ReleaseDate = UpdatedGameDto.ReleaseDate;
                existingGame.ImageUri = UpdatedGameDto.ImageUri;

                await repository.UpdateAsync(existingGame);

                return Results.NoContent();
            }
        );


        // Patch requests

        group.MapPatch("/{id}", async (IGamesRepository repository, int id, UpdateGameDto UpdatedGameDto) =>
            {
                Game? existingGame = await repository.GetAsync(id);

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

                    await repository.CreateAsync(game);
                    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.ID }, game);
                }

                existingGame.Name = UpdatedGameDto.Name;
                existingGame.Genre = UpdatedGameDto.Genre;
                existingGame.Price = UpdatedGameDto.Price;
                existingGame.ReleaseDate = UpdatedGameDto.ReleaseDate;
                existingGame.ImageUri = UpdatedGameDto.ImageUri;

                await repository.UpdateAsync(existingGame);

                return Results.NoContent();
            }
        );


        // Delete requests

        group.MapDelete("/{id}", async (IGamesRepository repository, int id) =>
            {
                Game? existingGame = await repository.GetAsync(id);

                if (existingGame is not null)
                {
                    await repository.DeleteAsync(id);
                }

                return Results.NoContent();
            }
        );

        return group;
    }
}