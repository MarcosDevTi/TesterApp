using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TesterApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
   // options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
   options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"))
    );

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
