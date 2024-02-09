using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using coe.dnd.api.Controllers;
using coe.dnd.api.ViewModels.Campaigns;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using coe.dnd.api.test.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace coe.dnd.api.test.Controllers;

[ExcludeFromCodeCoverage]
public class CampaignControllerTests
{
    private readonly ICampaignService _campaignService;
    private readonly IMapper _mapper;

    public CampaignControllerTests()
    {
        _campaignService = Substitute.For<ICampaignService>();
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public void GetCampaign_WhenCampaignFound_MapsAndReturns()
    {
        var id = 1;
        var campaign = new CampaignDto { Id = id };
        var campaignViewModel = new CampaignViewModel();
        var controller = new CampaignsController(_campaignService, _mapper);

        _campaignService.CampaignExists(id).Returns(true);
        _campaignService.GetCampaign(id).Returns(campaign);
        _mapper.Map<CampaignViewModel>(campaign).Returns(campaignViewModel);

        var actionResult = controller.GetCampaign(id);

        actionResult.AssertObjectResult<CampaignViewModel, OkObjectResult>().Should().BeSameAs(campaignViewModel);
        _campaignService.Received(1).GetCampaign(id);
        _mapper.Received(1).Map<CampaignViewModel>(campaign);
    }
}