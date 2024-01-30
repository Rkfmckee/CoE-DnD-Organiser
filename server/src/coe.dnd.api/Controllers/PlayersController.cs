using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }

    [HttpGet("{id}", Name = "GetById")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }
}