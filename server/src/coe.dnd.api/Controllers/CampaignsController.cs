using AutoMapper;
using coe.dnd.api.ViewModels.Campaigns;
using coe.dnd.dal.Models;
using coe.dnd.services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampaignsController : Controller
{
    private readonly ICampaignService _campaignService;
    private readonly IMapper _mapper;
    
    public CampaignsController(ICampaignService campaignService, IMapper mapper)
    {
        _campaignService = campaignService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IEnumerable<CampaignViewModel>), StatusCodes.Status200OK)]
    public IActionResult GetCampaigns()
    {
        var campaigns = _campaignService.GetCampaigns();
        if (campaigns == null) return NotFound();
        
        return Ok(_mapper.Map<IEnumerable<CampaignViewModel>>(campaigns));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CampaignViewModel), StatusCodes.Status200OK)]
    public IActionResult GetCampaignById(int id)
    {
        var campaign = _campaignService.GetCampaign(id);
        if (campaign == null) return NotFound();
        
        return Ok(_mapper.Map<CampaignViewModel>(campaign));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCampaignViewModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateCampaign(CreateCampaignViewModel campaignDetails)
    {
        var campaign = _mapper.Map<Campaign>(campaignDetails);
        _campaignService.CreateCampaign(campaign);
        
        return CreatedAtAction(nameof(CreateCampaign), null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UpdateCampaignViewModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateCampaign(int id, UpdateCampaignViewModel campaignDetails)
    {
        var campaign = _campaignService.GetCampaign(id);
        if (campaign == null) return NotFound();

        _mapper.Map(campaignDetails, campaign);

        _campaignService.UpdateCampaign(id, campaign);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteCampaign(int id)
    {
        _campaignService.DeleteCampaign(id);
        
        return NoContent();
    }
}