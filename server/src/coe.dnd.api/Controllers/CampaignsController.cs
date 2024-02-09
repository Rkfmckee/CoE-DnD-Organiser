using AutoMapper;
using coe.dnd.api.ViewModels.Campaigns;
using coe.dnd.services.DataTransferObjects;
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
        var campaignsData = _campaignService.GetCampaigns();
        if (campaignsData == null) return NotFound();
        
        return Ok(_mapper.Map<IList<CampaignViewModel>>(campaignsData));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CampaignViewModel), StatusCodes.Status200OK)]
    public IActionResult GetCampaignById(int id)
    {
        if (!_campaignService.CampaignExists(id)) return NotFound();

        var campaignData = _campaignService.GetCampaign(id);
        
        return Ok(_mapper.Map<CampaignViewModel>(campaignData));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateCampaign(CreateCampaignViewModel campaignDetails)
    {
        var campaignData = _mapper.Map<CampaignDto>(campaignDetails);
        _campaignService.CreateCampaign(campaignData);
        
        return CreatedAtAction(nameof(CreateCampaign), null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateCampaign(int id, UpdateCampaignViewModel campaignDetails)
    {
        if (!_campaignService.CampaignExists(id)) return NotFound();

        var campaignData = _mapper.Map<CampaignDto>(campaignDetails);
        _campaignService.UpdateCampaign(id, campaignData);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteCampaign(int id)
    {
        if (!_campaignService.CampaignExists(id)) return NotFound();
        
        _campaignService.DeleteCampaign(id);
        
        return NoContent();
    }
}