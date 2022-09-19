using Demo.BL.Interface;
using Demo.BL.Mapper;
using Demo.BL.Repository;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Demo.DAL.Extend;
using Demo.PL.Language;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.Configuration;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(SharedResource));
});





builder.Services.AddControllersWithViews().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
});





// Connection String
var connectionString = builder.Configuration.GetConnectionString("DemoConnection");

builder.Services.AddDbContext<DemoContext>(options =>
options.UseSqlServer(connectionString));

// Auto Mapper
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

// Transient (Take Instance With Every Operation)
//builder.Services.AddTransient<IDepartment, DepartmentRep>();

// Scoped (Take One Instance With All Operation)
builder.Services.AddScoped<IDepartmentRep, DepartmentRep>();
builder.Services.AddScoped<IEmployeeRep, EmployeeRep>();
builder.Services.AddScoped<ICountryRep, CountryRep>();
builder.Services.AddScoped<ICityRep, CityRep>();
builder.Services.AddScoped<IDistrictRep, DistrictRep>();


// SingleTone (Take One Instance Shared Between All Users)
//builder.Services.AddSingleton<IDepartment, DepartmentRep>();


//Default Page ( Login )
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/Login");
    });



// When this code work ( Error ==> Schema )
//builder.Services.AddAuthentication().AddIdentityCookies(o =>
//{
//    o.TwoFactorRememberMeCookie.Configure(a => a.Cookie.Expiration = new TimeSpan(10, 00, 00, 00));
//});


//Generate Token
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DemoContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);



builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    // Default Password settings.
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
}).AddEntityFrameworkStores<DemoContext>();


var app = builder.Build();


var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
                };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
});



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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=LogIn}/{id?}");

app.Run();

