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
        const int id = 1;
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
    
    [Fact]
    public void GetCampaign_WhenCampaignDoesntExist_ReturnsNotFound()
    {
        const int id = 1;
        var controller = new CampaignsController(_campaignService, _mapper);

        _campaignService.CampaignExists(id).Returns(false);

        var actionResult = controller.GetCampaign(id).Result;

        actionResult.Should().BeOfType<NotFoundResult>();
    }

    [Theory]
    [InlineData("campaignName", "campaignTheme", "writerName")]
    [InlineData(null, null, null)]
    public void GetCampaigns_WhenCampaignsFound_MapsAndReturns(string name, string theme, string writer)
    {
        const int id1 = 1;
        const int id2 = 2;
        var campaigns = new List<CampaignDto>
        {
            new CampaignDto { Id = id1, Name = name, Theme = theme, Writer = writer },
            new CampaignDto { Id = id2, Name = name, Theme = theme, Writer = writer }
        };
        var campaignViewModels = new List<CampaignViewModel>();
        var controller = new CampaignsController(_campaignService, _mapper);
        
        _campaignService.GetCampaigns(name, theme, writer).Returns(campaigns);
        _mapper.Map<IList<CampaignViewModel>>(campaigns).Returns(campaignViewModels);
        
        var actionResult = controller.GetCampaigns(name, theme, writer);
        
        actionResult.AssertObjectResult<IList<CampaignViewModel>, OkObjectResult>().Should().BeSameAs(campaignViewModels);
        _campaignService.Received(1).GetCampaigns(name, theme, writer);
        _mapper.Received(1).Map<IList<CampaignViewModel>>(campaigns);
    }

    [Theory]
    [InlineData("campaignName", "campaignTheme", "campaignDetails", "writerName")]
    public void CreateCampaign_WhenValidDataEntered_MappedAndSaved(string name, string theme, string details,
        string writer)
    {
        var campaign = new CampaignDto { Name = name, Theme = theme, Details = details, Writer = writer };
        var createCampaignViewModel = new CreateCampaignViewModel();
        var controller = new CampaignsController(_campaignService, _mapper);
        
        _mapper.Map<CampaignDto>(createCampaignViewModel).Returns(campaign);
        
        var actionResult = controller.CreateCampaign(createCampaignViewModel);

        actionResult.AssertObjectResult<CreatedAtActionResult>();
        _campaignService.Received(1).CreateCampaign(campaign);
        _mapper.Received(1).Map<CampaignDto>(createCampaignViewModel);
    }
    
    [Theory]
    [InlineData(1, "campaignName", "campaignTheme", "campaignDetails", "writerName")]
    public void UpdateCampaign_WhenValidDataEntered_MappedAndSaved(int id, string name, string theme, string details,
        string writer)
    {
        var campaign = new CampaignDto { Name = name, Theme = theme, Details = details, Writer = writer };
        var updateCampaignViewModel = new UpdateCampaignViewModel();
        var controller = new CampaignsController(_campaignService, _mapper);
        
        _campaignService.CampaignExists(id).Returns(true);
        _mapper.Map<CampaignDto>(updateCampaignViewModel).Returns(campaign);
        
        var actionResult = controller.UpdateCampaign(id, updateCampaignViewModel);

        actionResult.AssertResult<OkResult>();
        _campaignService.Received(1).UpdateCampaign(id, campaign);
        _mapper.Received(1).Map<CampaignDto>(updateCampaignViewModel);
    }
    
    [Fact]
    public void UpdateCampaign_WhenCampaignDoesntExist_ReturnsNotFound()
    {
        const int id = 1;
        var campaignDetails = new UpdateCampaignViewModel();
        var controller = new CampaignsController(_campaignService, _mapper);

        _campaignService.CampaignExists(id).Returns(false);

        var actionResult = controller.UpdateCampaign(id, campaignDetails);

        actionResult.Should().BeOfType<NotFoundResult>();
    }
    
    [Fact]
    public void DeleteCampaign_WhenCalledWithValidId_DeletedAndSaved()
    {
        const int id = 1;
        var controller = new CampaignsController(_campaignService, _mapper);

        _campaignService.CampaignExists(id).Returns(true);
        
        var actionResult = controller.DeleteCampaign(id);
        
        actionResult.AssertResult<NoContentResult>();
        _campaignService.Received(1).DeleteCampaign(id);
    }
    
    [Fact]
    public void DeleteCampaign_WhenCampaignDoesntExist_ReturnsNotFound()
    {
        const int id = 1;
        var controller = new CampaignsController(_campaignService, _mapper);

        _campaignService.CampaignExists(id).Returns(false);

        var actionResult = controller.DeleteCampaign(id);

        actionResult.Should().BeOfType<NotFoundResult>();
    }
}