using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using coe.dnd.api.Authentication;
using coe.dnd.api.ViewModels.Authentication;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace coe.dnd.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper, IConfiguration configuration)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
        _configuration = configuration;
    }
    
    [HttpPost]
    [AllowAnonymous]
    public ActionResult<AuthenticationResultViewModel> Authenticate([FromBody] AuthenticationRequestViewModel authenticationRequest)
    {
        var account = _authenticationService.Authenticate(authenticationRequest.Email, authenticationRequest.Password);
        if (account == null) return Unauthorized();
        
        return new AuthenticationResultViewModel
        {
            AccessToken = GenerateToken(account, 600, TokenTypes.AccessToken), 
            RefreshToken = GenerateToken(account, 18000, TokenTypes.RefreshToken)
        };
    }
    
    [HttpGet]
    public async Task<ActionResult<AuthenticationResultViewModel>> Refresh([FromServices] IAuthorizedPlayerProvider authorizedPlayerProvider)
    {
        var player = await authorizedPlayerProvider.GetLoggedInPlayer();
        if (player == null) return Unauthorized();
        
        return new AuthenticationResultViewModel
        {
            AccessToken = GenerateToken(player, 10, TokenTypes.AccessToken), 
            RefreshToken = GenerateToken(player, 18000, TokenTypes.RefreshToken)
        };
    }
    
    private string GenerateToken(PlayerDto player, int expirationTimeInMinutes, TokenTypes tokenType)
    {
        var secretKey = Encoding.UTF8.GetBytes(tokenType == TokenTypes.AccessToken ? 
            _configuration.GetValue<string>("JwtStrings:AccessToken") : 
            _configuration.GetValue<string>("JwtStrings:RefreshToken"));
        var securityKey = new SymmetricSecurityKey(secretKey);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var expiryTime = DateTime.UtcNow.AddMinutes(expirationTimeInMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, player.Id.ToString()),
                new Claim(ClaimTypes.Email, player.EmailAddress),
                new Claim(ClaimTypes.Role, "User")
            }),
            Expires = expiryTime,
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
         
        var tokenString = tokenHandler.WriteToken(jwtToken);
        return tokenString;
    }
}