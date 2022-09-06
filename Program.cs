using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using FriendList.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FriendListContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FriendListContext") ?? throw new InvalidOperationException("Connection string 'FriendListContext' not found.")));
builder.Services.AddDbContext<FriendListIdentityDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FriendListIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'FriendListIdentityDbContextConnection' not found.")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<FriendListIdentityDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FriendList}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
