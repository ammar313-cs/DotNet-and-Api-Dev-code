using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using jsonCrud.Models;

using jsonCrud.Data;
using jsonCrud.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.AddRazorPages();
//builder.Services.AddScoped<IDbRepo, PersonDBHandler>();
/*builder.Services.AddDbContext<PersonDBHandler>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("PersonDBHandler") ?? throw new InvalidOperationException("Connection string 'PersonDBHandler' not found."));
}); */
/*uilder.Services.AddDbContext<PersonDBHandler>(options =>
    options.UseInMemoryDatabase("PersonDatabase"));  */
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

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();

