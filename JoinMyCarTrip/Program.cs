using Data;
using JoinMyCarTrip.Data.Entities;
using JoinMyCarTrip.ModelBinders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    .Build();

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddApplicationDbContexts(config);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services
    .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<JoinMyCarTripDbContext>();


builder.Services.AddSession(options =>
options.Cookie.HttpOnly = true);

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
        options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider("dd.MM.yyyy"));
    });

//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    // This lambda determines whether user consent for non-essential 
//    // cookies is needed for a given request.
//    options.CheckConsentNeeded = context => true;
//    // requires using Microsoft.AspNetCore.Http;
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//});

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

app.UseRequestLocalization(options => 
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-GB");
    options.SupportedCultures = new[] { new CultureInfo("en-GB") };
    options.SupportedUICultures = new[] { new CultureInfo("en-GB") };
});

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
