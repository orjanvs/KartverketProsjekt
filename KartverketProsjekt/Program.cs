using KartverketProsjekt.Data;
using KartverketProsjekt.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KartverketProsjekt.Models.DomainModels;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.AddServerHeader = false;
});

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// connection string = "MariaDbConnection"
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

// Add database context to the services with delay to ensure that the database is
// ready before the application starts with Docker Compose. 
builder.Services.AddDbContext<KartverketDbContext>(options =>
   options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 5, 9)),
       mySqlOptions => mySqlOptions.EnableRetryOnFailure(
           maxRetryCount: 5, // Number of retry attempts
           maxRetryDelay: TimeSpan.FromSeconds(10), // Delay between retries
           errorNumbersToAdd: null // Additional error codes to retry on
       )));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<KartverketDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

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
        context.Database.Migrate();  // Only for use in dev with Docker Compose. 



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

// Security headers
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");

    context.Response.Headers.Append("Content-Security-Policy",
        "default-src 'self'; " +
        "script-src 'self' https://cdnjs.cloudflare.com https://unpkg.com https://kit.fontawesome.com https://ajax.googleapis.com 'unsafe-inline';  " + // Unsafe-inline only for dev. Remove for production. Use either hash or nonce.
        "style-src 'self' https://cdnjs.cloudflare.com https://unpkg.com https://fonts.googleapis.com 'unsafe-inline'; " + // Unsafe-inline only for dev. Remove for production. Use either hash or nonce.
        "font-src 'self' https://fonts.gstatic.com https://unpkg.com https://ka-f.fontawesome.com https://kit.fontawesome.com data:; " + 
        "img-src 'self' data: https:; " + 
        "connect-src 'self' https://api.kartverket.no https://ka-f.fontawesome.com; " + 
        "object-src 'none';");


    context.Response.Headers.Append("X-Frame-Options", "DENY");
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
    context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");

    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
