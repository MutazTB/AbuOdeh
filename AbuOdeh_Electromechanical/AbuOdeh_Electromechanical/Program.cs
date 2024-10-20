using AbuOdeh_Electromechanical.Models;
using AbuOdeh_Electromechanical.Repository;
using AbuOdeh_Electromechanical.Repository.Data;
using AbuOdeh_Electromechanical.Repository.Entities;
using AbuOdeh_Electromechanical.Repository.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager Configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddControllers()
//                    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


builder.Services.AddDbContext<AbuOdehDBContext>(options => {
    // Our DATABASE_URL from js days
    string connectionString = Configuration.GetConnectionString("Application");
    options.UseSqlServer(connectionString);
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AbuOdehDBContext>();
//builder.Services.AddAutoMapper();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICategorySvc, CategorySvc>();
builder.Services.AddScoped<IProductSvc, ProductSvc>();

builder.Services.AddLocalization();
builder.Services.AddControllersWithViews().AddViewLocalization()
            .AddDataAnnotationsLocalization();
builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AddAreaPageRoute("Admin", "/Account/Login", "/Admin/Account/Login");
    });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    CultureInfo[] supportedCultures = new[]
  {
        new CultureInfo("ar-JO"),
        new CultureInfo("en-US")
    };

    options.DefaultRequestCulture = new RequestCulture("ar-JO");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
        {
            new QueryStringRequestCultureProvider(),
            new CookieRequestCultureProvider()
        };
});

JWT_Secret.Key = builder.Configuration["JWT_Secret:Key"];
JWT_Secret.Issuer = builder.Configuration["JWT_Secret:Issuer"];
JWT_Secret.Audience = builder.Configuration["JWT_Secret:Audience"];


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
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();