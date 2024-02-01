using GameStore.Api.Entities;
namespace GameStore.Api.Repositories;

public class InMemGamesRepository : IGamesRepository
{
    private readonly List<Game> games = new(){
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

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await Task.FromResult(games);
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await Task.FromResult(games.Find(game => game.ID == id));
    }

    public async Task CreateAsync(Game game)
    {
        game.ID = games.Max(game => game.ID) + 1;
        games.Add(game);

        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Game updatedGame)
    {
        var index = games.FindIndex(game => game.ID == updatedGame.ID);
        games[index] = updatedGame;

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var index = games.FindIndex(game => game.ID == id);
        games.RemoveAt(index);

        await Task.CompletedTask;
    }
}