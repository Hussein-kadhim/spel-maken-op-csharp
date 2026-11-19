using Microsoft.AspNetCore.Mvc;
using Game.Api.Services;

namespace Game.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;

    public GameController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("status")]
    public IActionResult Status()
    {
        return Ok(new { message = "API werkt" });
    }

    [HttpGet("info")]
    public IActionResult Info()
    {
        return Ok(_gameService.GetInfo());
    }
}
