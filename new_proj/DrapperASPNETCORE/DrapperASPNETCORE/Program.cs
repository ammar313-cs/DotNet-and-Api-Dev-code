using DrapperASPNETCORE.Contracts;
using DrapperASPNETCORE.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);





builder.Services.AddRazorPages();

builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<IJokeRepository, JokeRepository>();

builder.Configuration.GetConnectionString("SqlConnection");

builder.Services.AddControllersWithViews();

builder.Services.AddControllers();

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


app.UseAuthorization();
app.MapControllers();

//app.MapAreaControllerRoute(name: "default", pattern: "{controller = Home}/{action = Index}/{id?}");

app.MapRazorPages();

app.Run();

