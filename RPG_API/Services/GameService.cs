namespace Game.Api.Services;

public record GameInfoDto(string Title, string Description, int TotalLevels);

public class GameService
{
    public GameInfoDto GetInfo()
    {
        return new GameInfoDto(
            Title: "Dungeon Crawler",
            Description: "Een gevaarlijke tocht door een duistere kerker vol monsters en schatten.",
            TotalLevels: 10
        );
    }
}
