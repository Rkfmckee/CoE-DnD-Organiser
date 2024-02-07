using AutoMapper;
using coe.dnd.api.ViewModels.Campaigns;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using Microsoft.AspNetCore.Mvc;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampaignsController : Controller
{
    private readonly IDndOrganiserDatabase _database;
    private readonly IMapper _mapper;
    
    public CampaignsController(IDndOrganiserDatabase database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IEnumerable<CampaignViewModel>), StatusCodes.Status200OK)]
    public IActionResult GetCampaigns()
    {
        var campaigns = _database.Get<Campaign>();
        if (campaigns == null) return NotFound();
        
        var campaignViewModels = _mapper.Map<IEnumerable<CampaignViewModel>>(campaigns);
        
        return Ok(campaignViewModels);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CampaignViewModel), StatusCodes.Status200OK)]
    public IActionResult GetCampaignById(int id)
    {
        var campaign = _database.Get<>();
        if (campaign == null) return NotFound();

        var campaignViewModel = _mapper.Map<CampaignViewModel>(campaign);
        
        return Ok(campaignViewModel);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCampaignViewModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateCampaign(CreateCampaignViewModel campaignDetails)
    {
        var campaign = _mapper.Map<Campaign>(campaignDetails);

        var addSuccessful = _database.Add(campaign);
        if (!addSuccessful) return BadRequest(campaignDetails);
        
        return CreatedAtAction(nameof(CreateCampaign), null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UpdateCampaignViewModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateCampaign(int id, UpdateCampaignViewModel campaignDetails)
    {
        var campaign = _database.Get(id);
        if (campaign == null) return NotFound();

        _mapper.Map(campaignDetails, campaign);

        var updateSuccessful = _database.Update(campaign);
        if (!updateSuccessful) return BadRequest(campaignDetails);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteCampaign(int id)
    {
        var campaign = _database.Get(id);
        if (campaign == null) return NotFound();
        
        var deleteSuccessful = _database.Delete(campaign);
        if (!deleteSuccessful) return BadRequest();
        
        return NoContent();
    }
}