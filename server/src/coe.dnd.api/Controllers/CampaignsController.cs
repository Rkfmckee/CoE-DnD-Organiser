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
    private readonly IRepository<Campaign> _campaignRepository;
    private readonly IMapper _mapper;
    
    public CampaignsController(IRepository<Campaign> campaignRepository, IMapper mapper)
    {
        _campaignRepository = campaignRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CampaignViewModel>), StatusCodes.Status200OK)]
    public IActionResult GetCampaigns()
    {
        var campaigns = _mapper.Map<IEnumerable<CampaignViewModel>>(_campaignRepository.GetAll());
        return Ok(campaigns);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CampaignViewModel), StatusCodes.Status200OK)]
    public IActionResult GetCampaignById(int id)
    {
        var campaign = _campaignRepository.Get(id);
        var campaignViewModel = _mapper.Map<CampaignViewModel>(campaign);
        
        return Ok(campaignViewModel);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCampaignViewModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateCampaign(CreateCampaignViewModel campaignDetails)
    {
        var campaign = _mapper.Map<Campaign>(campaignDetails);

        var addSuccessful = _campaignRepository.Add(campaign);
        if (!addSuccessful) return BadRequest(campaignDetails);
        
        return CreatedAtAction(nameof(CreateCampaign), null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UpdateCampaignViewModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateCampaign(int id, UpdateCampaignViewModel campaignDetails)
    {
        var campaign = _campaignRepository.Get(id);
        if (campaign == null) return NotFound();

        _mapper.Map(campaignDetails, campaign);

        var updateSuccessful = _campaignRepository.Update(campaign);
        if (!updateSuccessful) return BadRequest(campaignDetails);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteCampaign(int id)
    {
        var campaign = _campaignRepository.Get(id);
        if (campaign == null) return NotFound();
        
        var updateSuccessful = _campaignRepository.Delete(campaign);
        if (!updateSuccessful) return BadRequest();
        
        return NoContent();
    }
}