using coe.dnd.api;
using coe.dnd.dal.Contexts;
using coe.dnd.dal.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DndOrganiserContext>();
builder.Services.AddAutoMapper(config => config.AllowNullCollections = true, typeof(Program).Assembly);

builder.Services.AddScoped<IDndOrganiserDatabase, DndOrganiserContext>(_ => new DndOrganiserContext(EnvironmentVariables.DbConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
