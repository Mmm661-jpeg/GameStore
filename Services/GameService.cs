using GameStore.Models;
using System.Text.Json;

namespace GameStore.Services
{
  public class GameService
  {
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "games.json");

    private readonly IConfiguration _configuration;

    public GameService(IConfiguration configuration)
    {
      _configuration = configuration;
    }


    public List<Game> GetAllGames()
    {
      var jsonData = File.ReadAllText(_filePath);
      var gameList = JsonSerializer.Deserialize<List<Game>>(jsonData) ?? new List<Game>();

      var sasToken = _configuration["SASToken"];

      if(!string.IsNullOrEmpty(sasToken) && gameList.Count > 0)
      {

        gameList.ForEach(game =>
        {
            if (!string.IsNullOrEmpty(game.Cover) && !game.Cover.Contains(sasToken))
            {
                game.Cover += sasToken;
            }
        });

      }

      return gameList;
    }
  }
}
