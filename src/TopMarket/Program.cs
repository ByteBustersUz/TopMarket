using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Service.Helpers;
using TopMarket.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration
        .GetConnectionString("DefaultConnection"));
});

builder.Services.AddServices();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

PathHepler.WebRootPath = Path.GetFullPath("wwwroot");
PathHepler.CountryPath = Path.GetFullPath(builder.Configuration.GetValue<string>(("FilePath:CountryFilePaths")));
PathHepler.RegionPath = Path.GetFullPath(builder.Configuration.GetValue<string>(("FilePath:RegionFilePaths")));
PathHepler.DistrictPath = Path.GetFullPath(builder.Configuration.GetValue<string>(("FilePath:DictrictsFilePaths")));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
