using System.Net;
using coe.dnd.api.ViewModels.Campaigns;
using coe.dnd.integration.test.Base;
using coe.dnd.integration.test.TestUtilities;
using FluentAssertions;
using Xunit.Abstractions;

namespace coe.dnd.integration.test.Controllers;

[Collection("Integration")]
public class CampaignControllerTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _httpClient;

    public CampaignControllerTests(ITestOutputHelper testOutputHelper, IntegrationClassFixture integrationClassFixture)
    {
        _testOutputHelper = testOutputHelper;
        _httpClient = integrationClassFixture.Host.CreateClient();
    }
    
    [Fact]
    public async Task GetAllCampaigns_WhenCampaignsPresent_ReturnsOk()
    {
        var response = await _httpClient.GetAsync("/api/campaigns/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<CampaignViewModel[]>());
    }
}