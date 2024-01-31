using System.Net;
using coe.dnd.api.ViewModels.Campaigns;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampaignsController : Controller
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CampaignViewModel>), (int)HttpStatusCode.OK)]
    public IActionResult GetCampaigns()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CampaignViewModel), (int)HttpStatusCode.OK)]
    public IActionResult GetCampaignById(int id)
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public IActionResult CreateCampaign(CreateCampaignViewModel campaignDetails)
    {
        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult UpdateCampaign(int id, UpdateCampaignViewModel campaignDetails)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public IActionResult DeleteCampaign(int id)
    {
        return NoContent();
    }
}