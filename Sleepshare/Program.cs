using BusinessLogicLayer.IRepositorys;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Repositorys;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Voeg appsettings.json toe
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Voeg IConfiguration toe aan de DI-container
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Voeg AppConfiguration toe aan de DI-container
builder.Services.AddSingleton<AppConfiguration>();

// Configureer repositories en services
builder.Services.AddScoped<ISleepReviewRepository, SleepReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFollowerRepository, FollowerRepository>();
builder.Services.AddScoped<SleepReviewService>();
builder.Services.AddScoped<FollowerService>();
builder.Services.AddScoped<UserService>(); 


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
