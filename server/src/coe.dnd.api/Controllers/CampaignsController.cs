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
    public async Task<ActionResult<IList<CampaignViewModel>>> GetCampaigns([FromQuery] string name, [FromQuery] string theme, [FromQuery] string writer)
    {
        var campaignsData = await _campaignService.GetCampaignsAsync(name, theme, writer);
        if (campaignsData == null) return NotFound();
        
        return Ok(_mapper.Map<IList<CampaignViewModel>>(campaignsData));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CampaignViewModel), StatusCodes.Status200OK)]
    public async Task<ActionResult<CampaignViewModel>> GetCampaign(int id)
    {
        if (!(await _campaignService.CampaignExistsAsync(id))) return NotFound();

        var campaignData = await _campaignService.GetCampaignAsync(id);
        
        return Ok(_mapper.Map<CampaignViewModel>(campaignData));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> CreateCampaign(CreateCampaignViewModel campaignDetails)
    {
        var campaignData = _mapper.Map<CampaignDto>(campaignDetails);
        await _campaignService.CreateCampaignAsync(campaignData);
        
        return CreatedAtAction(nameof(CreateCampaign), null);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> UpdateCampaign(int id, UpdateCampaignViewModel campaignDetails)
    {
        if (!(await _campaignService.CampaignExistsAsync(id))) return NotFound();

        var campaignData = _mapper.Map<CampaignDto>(campaignDetails);
        await _campaignService.UpdateCampaignAsync(id, campaignData);
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteCampaign(int id)
    {
        if (!(await _campaignService.CampaignExistsAsync(id))) return NotFound();
        
        await _campaignService.DeleteCampaignAsync(id);
        
        return NoContent();
    }
}