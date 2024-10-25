using KartverketProsjekt.Data;
using KartverketProsjekt.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// connection string = "MariaDbConnection"
var connectionString = 
    builder.Configuration.GetConnectionString("DefaultConnection");

// Add database context to the services with delay to ensure that the database is
// ready before the application starts with Docker Compose. 
builder.Services.AddDbContext<KartverketDbContext>(options =>
   options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 5, 9)),
   mySqlOptions => mySqlOptions.EnableRetryOnFailure(

       maxRetryCount: 5,  // Number of retry attempts
       maxRetryDelay: TimeSpan.FromSeconds(10), // Delay between retries
       errorNumbersToAdd: null // Additional error codes to retry on
       )));


builder.Services.AddScoped<IMapReportRepository, MapReportRepository>();

var app = builder.Build();

// Apply migrations and create the database if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<KartverketDbContext>();
    context.Database.Migrate();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
