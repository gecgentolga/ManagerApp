using Infrastructure.DataAccess.ImportDataViaApi;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
public class PlayerImportController : Controller
{
    private IPlayerImport _playerImportHandler;

    public PlayerImportController(IPlayerImport playerImportHandler)
    {
        _playerImportHandler = playerImportHandler;
    }
    

    [HttpPost("ImportPlayers")]
    public async Task<IActionResult> ImportPlayers()
    {
        await _playerImportHandler.ImportPlayersFromApiAsync();

        return Ok("Players imported successfully!");
    }
}