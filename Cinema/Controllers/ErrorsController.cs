using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]

    public IActionResult Error()
    {
        return Problem();
    }
}