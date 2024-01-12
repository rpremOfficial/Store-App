using GameStore.Api.Entities;

const string GetGameEndpointName = "GetGame";

List<Game> games = new(){
    new Game(){
        ID = 1,
        Name = "FIFA 22",
        Genre = "Sports",
        Price = 299.99M,
        ReleaseDate = new DateTime(2021, 10, 1),
        ImageUri = "https://placehold.it/100x100"
    },
    new Game(){
        ID = 2,
        Name = "FIFA 21",
        Genre = "Sports",
        Price = 199.99M,
        ReleaseDate = new DateTime(2020, 10, 1),
        ImageUri = "https://placehold.it/100x100"
    },
    new Game(){
        ID = 3,
        Name = "FIFA 20",
        Genre = "Sports",
        Price = 99.99M,
        ReleaseDate = new DateTime(2019, 10, 1),
        ImageUri = "https://placehold.it/100x100"
    },
    new Game(){
        ID = 4,
        Name = "FIFA 19",
        Genre = "Sports",
        Price = 49.99M,
        ReleaseDate = new DateTime(2018, 10, 1),
        ImageUri = "https://placehold.it/100x100"
    }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// Get requests

app.MapGet("/", () => "Hello World!");

app.MapGet("/games", () => games);

app.MapGet("/games/{id}", (int id) =>
    {
        Game? game = games.Find(game => game.ID == id);

        if (game is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(game);
    }
)
.WithName(GetGameEndpointName);

// Post requests

app.MapPost("/games", (Game game) =>
    {
        game.ID = games.Max(game => game.ID) + 1;
        games.Add(game);

        return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.ID }, game);
    }
);


// Put requests

app.MapPut("/games/{id}", (int id, Game updatedGame) =>
    {
        Game? existingGame = games.Find(game => game.ID == id);

        if (existingGame is null)
        {
            return Results.NotFound();
        }

        existingGame.Name = updatedGame.Name;
        existingGame.Genre = updatedGame.Genre;
        existingGame.Price = updatedGame.Price;
        existingGame.ReleaseDate = updatedGame.ReleaseDate;
        existingGame.ImageUri = updatedGame.ImageUri;

        return Results.NoContent();
    }
);

app.Run();
