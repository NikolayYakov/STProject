using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using STProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using STProject.Data.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<STProjectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("STProjectConnectionString"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<STProjectContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI(); ;

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
