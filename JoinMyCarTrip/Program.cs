using Data;
using JoinMyCarTrip.Application.DependencyInjection;
using JoinMyCarTrip.Data.Entities;
using JoinMyCarTrip.ModelBinders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("JoinMyCarTrip");

builder.Services.AddDbContext<JoinMyCarTripDbContext>(options =>
    options.UseSqlServer(connectionString));

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

// Add services to the container.
builder.Services.AddApplicationServices(config);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services
    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<JoinMyCarTripDbContext>();


builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider("dd.MM.yyyy"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
