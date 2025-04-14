
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers;

[ApiController]
[Route("[controller]")]
public class KeyController : ControllerBase
{
    private readonly IConfiguration _config;

    public KeyController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("apikey")]
    public IActionResult GetApiKey()
    {
        var kvUri = _config["KeyVaultUri"] ?? "Not found..";
        return Ok(kvUri);
    }
}