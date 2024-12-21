using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Biblioteka.Controllers;


public class AuthorizationController : Controller
{
    [HttpPost("~/connect/token")]
    public IActionResult Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest();
        // Obsługa żądań tokenów
        return Ok();
    }
}
