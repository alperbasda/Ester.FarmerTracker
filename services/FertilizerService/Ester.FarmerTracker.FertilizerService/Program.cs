using Alp.MongoAdapter.Settings;
using Alp.ServiceExtensions;
using Ester.FarmerTracker.FertilizerService.Features._extensions;
using Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base._extensions;
using Ester.FarmerTracker.FertilizerService.Features.Fertilizers._base._extensions;
using Ester.FarmerTracker.FertilizerService.Features.KeepAlives.Alive;
using Ester.FarmerTracker.FertilizerService.Infrastructures._extensions;
using Ester.FarmerTracker.FertilizerService.Infrastructures._settings;
using Ester.FarmetTracker.Common.Extensions;
using Ester.FarmetTracker.Common.Middlewares;
using Ester.FarmetTracker.Common.Settings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


var cacheSettings = builder.Services.AddSettings<CacheSettings>(builder.Configuration);
var dbSettings = builder.Services.AddSettings<MongoSettings>(builder.Configuration);
var endpoints = builder.Services.AddSettings<ExternalServiceEndpoints>(builder.Configuration);
_ = builder.Services.AddSettings<JwtOptions>(builder.Configuration);

var asm = Assembly.GetExecutingAssembly();

builder.Services
    .AddHttpContextAccessor()
    .AddRedis(cacheSettings)
    .AddMappings(asm)
    .AddFluentValidations(asm)
    .AddMediatRService(Assembly.GetExecutingAssembly())
    .AddExceptionHandler(builder.Environment)
    .AddBusinessRules()
    .AddHttpClients(endpoints)
    .AddContext(dbSettings);

var app = builder.Build();

app.UseFileExceptionHandler();

app
    .AddKeepAliveEndpoint()
    .AddFertilizerEndpoints()
    .AddFertilizerHistoryEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
