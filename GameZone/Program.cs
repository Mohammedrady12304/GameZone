using GameZone.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source = (localdb)\\ProjectModels; Initial Catalog = GameZone; Integrated Security = True")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();//Ïí ÚÔÇä ãíØáÚÔ exeption ÈÊÇÚ Çá dependency
builder.Services.AddScoped<IDevicesService, DevicesService>();
builder.Services.AddScoped<IGamesService,GamesService>();
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
