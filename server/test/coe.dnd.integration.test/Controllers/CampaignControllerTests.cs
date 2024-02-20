using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using coe.dnd.api.ViewModels.Authentication;
using coe.dnd.api.ViewModels.Campaigns;
using coe.dnd.integration.test.Base;
using coe.dnd.integration.test.Models;
using coe.dnd.integration.test.TestUtilities;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        await SetAuthenticationHeaderTokenAsync();
        
        var response = await _httpClient.GetAsync("/api/campaigns/");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<CampaignViewModel[]>());
    }
    
    [Fact]
    public async Task GetACampaignById_WhenCampaignPresent_ReturnsOk()
    {
        await SetAuthenticationHeaderTokenAsync();
        
        const int id = 1;
        var response = await _httpClient.GetAsync($"/api/campaigns/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var value = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(value.VerifyDeSerialization<CampaignViewModel>());

        Assert.Contains("1", value);
        Assert.Contains("Campaign 1", value);
        Assert.Contains("Theme 1", value);
        Assert.Contains("Campaign 1 details", value);
        Assert.Contains("Writer 1", value);
    }
    
    [Fact]
    public async Task CreateACampaign_WhenCampaignDetailsValidAndPresent_ReturnsCreated()
    {
        await SetAuthenticationHeaderTokenAsync();
        
        var newCampaign = new CreateCampaignViewModel
        {
            Name = "New campaign name",
            Theme = "New original theme",
            Details = "Details of the new campaign",
            Writer = "Me"
        };
        
        var response = await _httpClient.PostAsJsonAsync($"/api/campaigns/", newCampaign);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateAnAccount_WhenAccountDetailsInvalid_ReturnsValidationError()
    {
        await SetAuthenticationHeaderTokenAsync();

        var newCampaign = new CreateCampaignViewModel();
        
        var response = await _httpClient.PostAsJsonAsync("/api/campaigns/", newCampaign);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("Name", "'Name' must not be empty.");
        result.Errors.CheckIfErrorPresent("Theme", "'Theme' must not be empty.");
        result.Errors.CheckIfErrorPresent("Writer", "'Writer' must not be empty.");
        
        _testOutputHelper.WriteLine(value);
    }
    
    [Fact]
    public async Task UpdateACampaign_WhenNewCampaignDetails_ValidAndPresent_ReturnsOk()
    {
        await SetAuthenticationHeaderTokenAsync();
        
        const int id = 2;

        var updateCampaign = new UpdateCampaignViewModel()
        {
            Name = "New name",
            Theme = "The new theme",
            Writer = "A new writer"
        };

        var response = await _httpClient.PutAsJsonAsync($"/api/campaigns/{id}", updateCampaign);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task UpdateACampaign_WhenNewCampaignDetailsEmpty_ReturnsError()
    {
        await SetAuthenticationHeaderTokenAsync();

        const int id = 2;

        var updateCampaign = new UpdateCampaignViewModel();

        var response = await _httpClient.PutAsJsonAsync($"/api/campaigns/{id}", updateCampaign);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var value = await response.Content.ReadAsStringAsync();
        
        var result = value.VerifyDeSerialize<ValidationModel>();
        result.Errors.CheckIfErrorPresent("Name", "'Name' must not be empty.");
        
        _testOutputHelper.WriteLine(value);
    }
    
    [Fact]
    public async Task DeleteAnAccount_WhenAccountFound_ThenDeleted_ReturnsOk()
    {
        await SetAuthenticationHeaderTokenAsync();

        const int id = 3;

        var response = await _httpClient.DeleteAsync($"/api/campaigns/{id}");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    private async Task SetAuthenticationHeaderTokenAsync()
    {
        var authenticationRequest = new AuthenticationRequestViewModel
        {
            Email = "player1@dnd.com",
            Password = "Password1"
        };
        
        var authenticationResponse = await _httpClient.PostAsJsonAsync("/api/authentication/", authenticationRequest);
        var authenticationToken = JObject.Parse(await authenticationResponse.Content.ReadAsStringAsync())["accessToken"]?.ToString();
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
    }
}