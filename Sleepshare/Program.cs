using BusinessLogicLayer.IRepositorys;
using DataAccessLayer.Repositorys;
using BusinessLogicLayer.Services;
using System.Configuration;
using DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);


// Voeg appsettings.json toe
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Configureer andere services (bijv. repositories)
builder.Services.AddScoped<ISleepReviewRepository, SleepReviewRepository>();


//// Voeg services toe aan de DI-container.
//builder.Services.AddSingleton<ISleepReviewRepository, SleepReviewRepository>(); // Registratie van de repository
//builder.Services.AddSingleton<SleepReviewService>(); // Registratie van de service

var configuration =  new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

builder.Services.AddSingleton(configuration);
builder.Services.AddSingleton<AppConfiguration>();

// Add services to the container.
    builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
