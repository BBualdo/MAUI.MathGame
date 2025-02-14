using MathGame.Models;
using SQLite;

namespace MathGame.Data;

public class GamesRepository
{
    string _dbPath;
    SQLiteConnection _connection;

    public GamesRepository(string dbPath)
    {
        _dbPath = dbPath;
        Init();
    }

    private void Init()
    {
        using (_connection = new SQLiteConnection(_dbPath))
            _connection.CreateTable<Game>();
    }

    public List<Game> GetGames()
    {
        using (_connection = new SQLiteConnection(_dbPath))
            return _connection.Table<Game>().ToList();
    }

    public void AddGame(Game game)
    {
        using (_connection = new SQLiteConnection(_dbPath))
            _connection.Insert(game);
    }

    public void DeleteGame(int id)
    {
        using (_connection = new SQLiteConnection(_dbPath))
        {
            var game = _connection.Find<Game>(id);
            _connection.Delete(game);
        }
    }
}