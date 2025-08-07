using Campus_Book_Connect.Data;
using Campus_Book_Connect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddControllersWithViews();

//Configure SQL Server Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CBookDB")));

//Enable Session support
builder.Services.AddDistributedMemoryCache(); // Session storage in memory
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // User stays logged in for 30 mins
    options.Cookie.HttpOnly = true; // More secure
    options.Cookie.IsEssential = true; // Required for session to work without consent
});






var app = builder.Build();

//Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

//Enable session middleware before MVC
app.UseSession();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    Campus_Book_Connect.Data.DbInitializer.Seed(context);
}


app.Run();
