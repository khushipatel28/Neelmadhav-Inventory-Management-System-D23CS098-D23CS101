using Clgproject.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ClgDbContext>(options=>options.UseSqlServer(builder.Configuration
    .GetConnectionString("DBConnection")
    ));

//// Register the background service
//builder.Services.AddHostedService<SupplierService>();


builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".YourAppName.Session";
    options.IdleTimeout = System.TimeSpan.FromSeconds(3600);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Seed data when the application starts
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ClgDbContext>();
//    DbInitializer.Initialize(dbContext); // Call the data seeding method
//}

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
app.UseSession();

app.MapControllerRoute(
    name: "default",    
    pattern: "{controller=ComapnyLogin}/{action=Login}/{id?}");

app.Run();
