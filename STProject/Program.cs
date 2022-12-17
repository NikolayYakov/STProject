using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using STProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using STProject.Data.Models;
using STProject.Repositories.Interfaces;
using STProject.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<STProjectContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("STProjectConnectionString"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => 
options.Password = new PasswordOptions
{
    RequireDigit = false,
    RequiredLength = 6,
    RequireLowercase = false,
    RequireUppercase = false,
    RequireNonAlphanumeric = false,
})
.AddEntityFrameworkStores<STProjectContext>()
.AddDefaultTokenProviders()
.AddDefaultUI(); 

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Community/AddCommunity");
    options.Conventions.AuthorizePage("/Community/AllCommunities");
    options.Conventions.AuthorizePage("/Profile");
});

builder.Services.AddScoped<IUserRepository, UserRepo>();

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
