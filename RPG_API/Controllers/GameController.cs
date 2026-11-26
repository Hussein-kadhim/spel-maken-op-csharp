using Microsoft.AspNetCore.Mvc;
using Game.Api.Services;

namespace Game.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;

    // GameService geïnjecteerd via de constructor
    public GameController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    [HttpGet("status")]
    [HttpGet("/api/status")]
    public IActionResult Status()
    {
        // Hier roepen we de GameService aan voor de status
        return Ok(new { message = _gameService.GetStatus() });
    }

    [HttpGet("info")]
    public IActionResult Info()
    {
        // Hier roepen we de GameService aan voor de spelinformatie
        return Ok(_gameService.GetInfo());
    }
}
