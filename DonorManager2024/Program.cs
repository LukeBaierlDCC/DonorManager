using DonorManager2024.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DonorManager.Models;
using System.Data;
using Microsoft.AspNetCore.Mvc.Authorization;
using DonorManager2024.Models.CampaignsRelated;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.CodeAnalysis.Elfie.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Lockout.AllowedForNewUsers = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
    .AddRoles<IdentityRole>()/*<---line created 2/8/24 for authorization*/
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication();
//can add another AddAuthorization method with options (page 610)
//builder.Services.AddAuthorizationBuilder().AddPolicy("CanEnterRoles", policyBuilder => policyBuilder.RequireClaim("Admin"));
builder.Services.AddAuthorization(options =>
{
    //options.FallbackPolicy = new AuthorizationPolicyBuilder()
    //.RequireAuthenticatedUser()
    //.Build();

    options.AddPolicy("RequireAdminRole",
        policy => policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages();

//builder.Services.AddScoped<ICSVService>();

//builder.Services.AddControllers(config =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//    .RequireAuthenticatedUser()
//    .Build();
//    config.Filters.Add(new AuthorizeFilter(policy));
//});

builder.Services.AddControllersWithViews();

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

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "ABData", "Client" };

    string email = "luke.baierl@abdata.com";
    string password = "Test123!!";

    var adminRole = await roleManager.FindByNameAsync("Admin");       

    if (adminRole == null)
    {
        var role = new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" };

        var result = await roleManager.CreateAsync(role);
        
        if (!result.Succeeded)
        {
        }
    }
    
    //save FindByEmailAsync and check its value if it's not null
    var identityUser = await userManager.FindByEmailAsync(email);

    if (identityUser == null)
    {
        var user = new ApplicationUser { UserName = email, Email = email };
        
        user.UserName = email;        
        user.Email = email;

        await userManager.CreateAsync(user, password);
        
        await userManager.AddToRoleAsync(user, "Admin");
    }
    else
    {
        //user exists and does not return null and has admin role
        //add role async
        await userManager.AddToRoleAsync(identityUser, "Admin");
    }

}

app.Run();
