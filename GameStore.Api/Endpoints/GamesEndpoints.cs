using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        InMemGamesRepository repository = new();

        var group = routes.MapGroup("/games")
            .WithParameterValidation();


        // Get requests

        group.MapGet("/", () => repository.GetAll());

        group.MapGet("/{id}", (int id) =>
            {
                Game? game = repository.Get(id);
                return game is not null ? Results.Ok(game) : Results.NotFound();
            }
        )
        .WithName(GetGameEndpointName);

        // Post requests

        group.MapPost("/", (Game game) =>
            {
                repository.Create(game);
                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.ID }, game);
            }
        );


        // Put requests

        group.MapPut("/{id}", (int id, Game updatedGame) =>
            {
                Game? existingGame = repository.Get(id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                existingGame.Name = updatedGame.Name;
                existingGame.Genre = updatedGame.Genre;
                existingGame.Price = updatedGame.Price;
                existingGame.ReleaseDate = updatedGame.ReleaseDate;
                existingGame.ImageUri = updatedGame.ImageUri;

                repository.Update(existingGame);

                return Results.NoContent();
            }
        );


        // Patch requests

        group.MapPatch("/{id}", (int id, Game game) =>
            {
                Game? existingGame = repository.Get(id);

                if (existingGame is null)
                {
                    repository.Create(game);
                    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.ID }, game);
                }

                existingGame.Name = game.Name;
                existingGame.Genre = game.Genre;
                existingGame.Price = game.Price;
                existingGame.ReleaseDate = game.ReleaseDate;
                existingGame.ImageUri = game.ImageUri;

                repository.Update(existingGame);

                return Results.NoContent();
            }
        );


        // Delete requests

        group.MapDelete("/{id}", (int id) =>
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