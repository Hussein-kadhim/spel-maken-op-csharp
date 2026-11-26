namespace Game.Api.Services;

public record GameInfoDto(string Title, string Description, int TotalLevels);

public class GameService
{
    public string GetStatus()
    {
        return "API werkt";
    }

    public GameInfoDto GetInfo()
    {
        return new GameInfoDto(
            Title: "Dungeon Crawler",
            Description: "Een gevaarlijke tocht door een duistere kerker vol monsters en schatten.",
            TotalLevels: 10
        );
    }
}
