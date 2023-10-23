using BlogProject.Data;
using BlogProject.Helpers;
using BlogProject.Models;
using BlogProject.Services;
using BlogProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = ConnectionHelper.GetConnectionString(builder.Configuration) ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//  options.UseSqlServer(connectionString));

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Services.AddDatabaseDeveloperPageExceptionFilter();



//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDefaultIdentity<BlogUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.AddControllersWithViews();

//Register my custom DataService class
builder.Services.AddScoped<DataService>();

//Register a preconfigured instace of the MailSettings class
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IBlogEmailSender, EmailService>();

//Register our Image Service
builder.Services.AddScoped<IImageService, BasicImageService>();

//Register the Slug Service
builder.Services.AddScoped<ISlugService, BasicSlugService>();

//Register Search Service
builder.Services.AddScoped<BlogSearchService>();

var app = builder.Build();



//Using the Custom DataService
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var dataService = services.GetRequiredService<DataService>();
    await dataService.ManageDataAsync();
}

var scope = app.Services.CreateScope();

//Keep database updated with the latest migrations
await DataHelper.ManageDataAsync(scope.ServiceProvider);

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

app.UseAuthorization();

app.MapControllerRoute(
    name:"SlugRoute",
    pattern:"BlogPosts/UrlFriendly/{slug}",
    defaults: new { controller = "Posts", action = "Details" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();
