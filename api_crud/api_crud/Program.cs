using api_crud.Handlers;
using jsonCrud.Data;
using jsonCrud.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using api_crud.Configuration;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using api_crud.validation_handler;
using Microsoft.Extensions.Options;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("https://localhost:4200")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});



builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuntheticationHandler>("BasicAuthentication", null); //Add basic Aunthetication

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DynamicRolePolicy", policy =>
    {
        policy.Requirements.Add(new DynamicRoleRequirement());
        // You can add additional requirements or authentication schemes if needed.
    });
});// Registeration of DynamicRolePolicy for authorization




builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();




builder.Services.AddScoped<IDbRepo, PersonDBHandler>();
builder.Services.AddScoped<IUDbRepo, UserDBHandler>();
builder.Services.AddScoped<UserDBHandler>();





// Learn more about configuring Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(opt => 
{
    opt.DefaultApiVersion = new ApiVersion(1,0); //setting default version
    opt.AssumeDefaultVersionWhenUnspecified = true;  
    opt.ApiVersionReader = new HeaderApiVersionReader("api-version");//header version
    opt.ReportApiVersions = true;  // include API version in response 
    
});







// Register the ControllerAccessConfig as a scoped service

var controllerAccessConfig = builder.Configuration.GetSection("ControllerAccessConfig").Get<ControllerAccessConfig>();
builder.Services.AddSingleton(controllerAccessConfig);
builder.Services.AddSingleton<DynamicValidationService>();




// Register the DynamicRoleAuthorizationHandler
//builder.Services.AddScoped<DynamicRoleAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, DynamicRoleAuthorizationHandler>();






var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

//app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

/*app.UseEndpoints(endpoints =>
            {
               // Map version 1 of the controller
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/1/{controller}/{action}/{id?}",
                    defaults: new { controller = "PersonController", action = "Get" });

                // Map version 2 of the controller
                endpoints.MapControllerRoute(
                    name: "version2",
                    pattern: "api/2/{controller}/{action}/{id?}",
                    defaults: new { controller = "PersonControllerV2", action = "Get" });
                // Map version based on header
                endpoints.MapControllerRoute(
                    name: "headerVersion",
                    pattern: "api/{version}/{controller}/{action}/{id?}",
                    defaults: new { controller = "PersonController", action = "Get" },
                    constraints: new { version = @"v\d+" });

                 

                endpoints.MapControllers();
            }); */



app.Run();
