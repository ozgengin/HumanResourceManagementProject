using Infrastructure.Data;
//using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Interfaces;
using Project.Services;
using ApplicationCore.Attributes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<HumanResourceContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HumanResourceContext")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAdminViewModelService, AdminIndexViewModelService>();

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
app.UseRequestLocalization("en-US");
app.UseRouting();



app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/login");
    }
    else
    {
        await next.Invoke();
    }
});
app.MapControllerRoute(
       name: "login",
       pattern: "{area:exists}/{controller=Login}/{action=SignOut}/{id?}",
       defaults: new { area = "Login" });
app.MapControllerRoute(
           name: "admin",
           pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var identityContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var HumanResourceContext = scope.ServiceProvider.GetRequiredService<HumanResourceContext>();
    await HumanResourceContextSeed.SeedAsync(HumanResourceContext);
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await roleManager.CreateAsync(new IdentityRole("admin"));
    await roleManager.CreateAsync(new IdentityRole("Worker"));
    await roleManager.CreateAsync(new IdentityRole("Manager"));
    var adminUser = new ApplicationUser()
    {
        UserName = "admin",
        Name = "admin",
        Surname = "admin",
        Email = "admin@admin.com",
        IdentificationNumber = 11799900352,
        BirthDate = DateTime.Now,
        BirthPlace = "admin",
        Address = "admin",
        Phone = Methods.FormatPhoneNumber("5336669933"),
        ImageName = "profile-img.jpg",
        HasLogin = false,


    };
    await userManager.CreateAsync(adminUser, "Ankara1.");
    await userManager.AddToRoleAsync(adminUser, "admin");
}



app.Run();
