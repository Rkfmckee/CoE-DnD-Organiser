using coe.dnd.api;
using coe.dnd.api.Authentication;
using coe.dnd.dal.Contexts;
using coe.dnd.dal.Interfaces;
using coe.dnd.services.Interfaces;
using coe.dnd.services.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using AuthenticationService = coe.dnd.services.Services.AuthenticationService;
using IAuthenticationService = coe.dnd.services.Interfaces.IAuthenticationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
builder.Services.AddAutoMapper(config => config.AllowNullCollections = true, typeof(Program).Assembly, typeof(CampaignService).Assembly);

builder.Services.AddAuthentication(string.Empty).AddScheme<AuthenticationSchemeOptions, AccessAuthenticationFilter>(string.Empty, options => {});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(string.Empty, policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddFluentValidation(s =>
    s.RegisterValidatorsFromAssemblyContaining<Program>()
);

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IDndOrganiserDatabase, DndOrganiserContext>(_ => new DndOrganiserContext(EnvironmentVariables.DbConnectionString));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IGameMasterService, GameMasterService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();

app.Run();

public partial class Program { };