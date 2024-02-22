using Microsoft.AspNetCore.Mvc;

namespace LondonStock.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiVersion("1.0")]
    public IActionResult Error(){
        return Problem();
    }
}