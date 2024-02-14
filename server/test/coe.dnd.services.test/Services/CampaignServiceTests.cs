using AutoFixture;
using AutoMapper;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Profiles;
using coe.dnd.services.Services;
using FluentAssertions;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace coe.dnd.services.test.Services;

public class CampaignServiceTests
{
    private readonly IDndOrganiserDatabase _database;
    private readonly IMapper _mapper;
    private readonly IFixture _fixture;
    
    public CampaignServiceTests()
    {
        _database = Substitute.For<IDndOrganiserDatabase>();
        _mapper = new Mapper(
            new MapperConfiguration(config => {
                config.AddProfile<CampaignProfile>();
            })
        );
        _fixture = new Fixture();
    }
    
    [Fact]
    public async Task CampaignExists_WhenCampaignExists_ReturnsTrue()
    {
        const int id = 1;
        var campaign = new Campaign() { Id = id };
        var campaigns = new List<Campaign> { campaign };
        var campaignService = new CampaignService(_database, _mapper);
        
        _database.Get<Campaign>().Returns(campaigns.AsQueryable().BuildMock());

        var result = await campaignService.CampaignExistsAsync(id);

        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task GetCampaign_WhenCampaignExists_ReturnsAccount()
    {
        const int id = 1;
        var campaign = new Campaign() { Id = id };
        var campaigns = new List<Campaign> { campaign };
        var campaignService = new CampaignService(_database, _mapper);
        
        _database.Get<Campaign>().Returns(campaigns.AsQueryable().BuildMock());

        var result = await campaignService.GetCampaignAsync(id);
        
        result.Should().BeEquivalentTo(campaign, options => options.ExcludingMissingMembers());
    }
    
    [Fact]
    public async Task GetAccounts_WhenCampaignsExist_ReturnsAccountList()
    {
        var campaignList = _fixture.Build<Campaign>().Without(c => c.Games).CreateMany();
        var campaignService = new CampaignService(_database, _mapper);

        _database.Get<Campaign>().Returns(campaignList.AsQueryable().BuildMock());
        
        var result = await campaignService.GetCampaignsAsync();
        
        result.Should().BeEquivalentTo(campaignList, options => options.ExcludingMissingMembers());
    }
    
    [Fact]
    public async Task CreateCampaign_ValidDataEntered_MappedAndSaved()
    {
        const int id = 1;

        var campaign = new CampaignDto()
        {
            Id = id,
            Name = "Campaign name",
            Theme = "Campaign theme",
            Details = "Campaign details",
            Writer = "Writer name"
        };
        var campaignService = new CampaignService(_database, _mapper);

        // Act
        await campaignService.CreateCampaignAsync(campaign);
        
        // Assert
        await _database.Received(1).SaveChangesAsync();
        _database.Received(1).Add(Arg.Is<Campaign>(x => x.Name == campaign.Name));
    }
    
    [Theory]
    [InlineData("Campaign name", "Campaign theme", "Campaign details", "Writer name")]
    [InlineData(null, null, null, null)]
    public async Task UpdateCampaign_ValidDataEntered_MappedAndSaved(string name, string theme, string details, string writer)
    {
        var campaigns = _fixture.Build<Campaign>().Without(x => x.Games).CreateMany();
        var campaign = campaigns.First();
        var campaignData = _mapper.Map<CampaignDto>(campaigns.First());
        var campaignService = new CampaignService(_database, _mapper);
        
        _database.Get<Campaign>().Returns(campaigns.AsQueryable().BuildMock());
        _database.When(database => database.SaveChanges()).Do(info =>
        {
            campaigns.First().Should().BeEquivalentTo(campaignData, options => options.ExcludingMissingMembers());
        });
        
        await campaignService.UpdateCampaignAsync(campaign.Id, campaignData);
        
        await _database.Received(1).SaveChangesAsync();
    }
    
    [Fact]
    public async Task DeleteCampaign_ValidIdEntered_MappedAndSaved()
    {
        var campaigns = _fixture.Build<Campaign>().Without(x => x.Games).CreateMany();
        var campaignService = new CampaignService(_database, _mapper);

        _database.Get<Campaign>().Returns(campaigns.AsQueryable().BuildMock());
        
        await campaignService.DeleteCampaignAsync(campaigns.First().Id);
        
        _database.Received(1).Get<Campaign>();
        _database.Received(1).Delete(campaigns.First());
        await _database.Received(1).SaveChangesAsync();
    }
}