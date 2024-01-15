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

    public IEnumerable<Game> GetAll()
    {
        return games;
    }

    public Game? Get(int id)
    {
        return games.Find(game => game.ID == id);
    }

    public void Create(Game game)
    {
        game.ID = games.Max(game => game.ID) + 1;
        games.Add(game);
    }

    public void Update(Game updatedGame)
    {
        var index = games.FindIndex(game => game.ID == updatedGame.ID);
        games[index] = updatedGame;
    }

    public void Delete(int id)
    {
        var index = games.FindIndex(game => game.ID == id);
        games.RemoveAt(index);
    }
}