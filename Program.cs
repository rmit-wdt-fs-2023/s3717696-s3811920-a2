using MCBA_Web.Controllers;
using MCBA_Web.Data;
using MCBA_Web.Services;
using McbaExample.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connect to DB
builder.Services.AddDbContext<MCBAContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(MCBAContext))));


// Register Services
builder.Services.AddScoped<ICustomerService, CustomerService>();

// Register Repositories
//builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Seed data.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
