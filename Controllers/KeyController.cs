using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
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
    public async Task<IActionResult> GetApiKey()
    {
        var kvUri = _config["KeyVaultUri"] ?? "Not found..";

        var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        KeyVaultSecret secret = await client.GetSecretAsync("TheApiKey");

        return Ok(secret.Value);
    }
}