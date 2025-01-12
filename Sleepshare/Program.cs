using BusinessLogicLayer.IRepositorys;
using DataAccessLayer.Repositorys;
using BusinessLogicLayer.Services;
using Microsoft.Extensions.Configuration;
using DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

// Voeg appsettings.json toe
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Configureer andere services (bijv. repositories)
builder.Services.AddScoped<ISleepReviewRepository, SleepReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// configureer services
//builder.Services.AddSingleton<AppConfiguration>();
//builder.Services.AddScoped<ISleepReviewRepository, SleepReviewRepository>();
//builder.Services.AddScoped<SleepReviewService, SleepReviewService>();



var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

builder.Services.AddSingleton(configuration);
builder.Services.AddSingleton<AppConfiguration>();


// Add services to the container.
builder.Services.AddControllersWithViews();

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add session
builder.Services.AddSession();

var app = builder.Build();

// Enable session middleware
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SleepReview}/{action=SleepFeed}/{id?}");

app.Run();

