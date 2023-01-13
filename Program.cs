using MCBA_Web.Controllers;
using MCBA_Web.Data;
using MCBA_Web.Repositories;
using MCBA_Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connect to DB
builder.Services.AddDbContext<MCBAContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(MCBAContext))));


// Add Services
builder.Services.AddScoped<ICustomerService, CustomerService>();

// Add Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();


// Check if DB is empty

// Register Controllers


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
