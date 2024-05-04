using WebApi.Controllers;
using WebApi.DbHandler;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Hosting;


//string customUrl = "http://web_api";

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseUrls(customUrl);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(opt => 
{
    opt.DefaultApiVersion = new ApiVersion(1,0); //setting default version
    opt.AssumeDefaultVersionWhenUnspecified = true;  
    opt.ReportApiVersions = true;  // include API version in response 
});

builder.Services.AddScoped<JsonFileDb>();



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting(); // Add routing middleware

app.MapControllers();


app.Run();
