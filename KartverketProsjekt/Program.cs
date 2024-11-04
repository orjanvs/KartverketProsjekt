using KartverketProsjekt.Data;
using KartverketProsjekt.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KartverketProsjekt.Models.DomainModels;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// connection string = "MariaDbConnection"
var connectionString = 
    builder.Configuration.GetConnectionString("DefaultConnection");

var authConnectionString =
    builder.Configuration.GetConnectionString("KartverketAuthConnection");


// Add database context to the services with delay to ensure that the database is
// ready before the application starts with Docker Compose. 
builder.Services.AddDbContext<KartverketDbContext>(options =>
   options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 5, 9)),
       mySqlOptions => mySqlOptions.EnableRetryOnFailure(
           maxRetryCount: 5, // Number of retry attempts
           maxRetryDelay: TimeSpan.FromSeconds(10), // Delay between retries
           errorNumbersToAdd: null // Additional error codes to retry on
       )));

builder.Services.AddDbContext<AuthDbContext>(options =>
   options.UseMySql(authConnectionString, new MySqlServerVersion(new Version(10, 5, 9)),
       mySqlOptions => mySqlOptions.EnableRetryOnFailure(
           maxRetryCount: 5, // Number of retry attempts
           maxRetryDelay: TimeSpan.FromSeconds(10), // Delay between retries
           errorNumbersToAdd: null // Additional error codes to retry on
       )));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>(); 

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddScoped<IMapReportRepository, MapReportRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<KartverketDbContext>();
        context.Database.Migrate();

        var authContext = services.GetRequiredService<AuthDbContext>();
        authContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        // Log the error (you can use a logging framework here)
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
        // Optionally, rethrow or handle the exception as needed
    }
}

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
