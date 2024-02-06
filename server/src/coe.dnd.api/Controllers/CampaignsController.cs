using coe.dnd.api.ViewModels.Campaigns;
using coe.dnd.dal.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampaignsController : Controller
{
    #region Fields
    
    private readonly CoeDndOrganiserContext _context;
    
    #endregion

    #region Constructors

    public CampaignsController(CoeDndOrganiserContext context)
    {
        _context = context;
    }
    
    #endregion
    
    #region Actions
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CampaignViewModel>), StatusCodes.Status200OK)]
    public IActionResult GetCampaigns()
    {
        var campaigns = _context.Campaigns.ToList();
        return Ok(campaigns);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CampaignViewModel), StatusCodes.Status200OK)]
    public IActionResult GetCampaignById(int id)
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCampaignViewModel), StatusCodes.Status201Created)]
    public IActionResult CreateCampaign(CreateCampaignViewModel campaignDetails)
    {
        return CreatedAtAction(nameof(CreateCampaign), campaignDetails);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateCampaign(int id, UpdateCampaignViewModel campaignDetails)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteCampaign(int id)
    {
        return NoContent();
    }
    
    #endregion
}