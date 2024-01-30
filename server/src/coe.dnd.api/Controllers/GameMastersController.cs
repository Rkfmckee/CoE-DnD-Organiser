using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

public class GameMastersController : Controller
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}