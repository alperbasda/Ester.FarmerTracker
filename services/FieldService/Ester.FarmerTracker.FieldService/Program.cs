using Alp.MsSqlAdapter.Settings;
using Alp.ServiceExtensions;
using Ester.FarmerTracker.FieldService.Features._extensions;
using Ester.FarmerTracker.FieldService.Features.Crops._base._extensions;
using Ester.FarmerTracker.FieldService.Features.Customers._base._extensions;
using Ester.FarmerTracker.FieldService.Features.Fields._base._extensions;
using Ester.FarmerTracker.FieldService.Features.Harvests._base._extensions;
using Ester.FarmerTracker.FieldService.Features.KeepAlives.Alive;
using Ester.FarmetTracker.Common.Extensions;
using Ester.FarmetTracker.Common.Middlewares;
using Ester.FarmetTracker.Common.Settings;
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
var mssqlSettings = builder.Services.AddSettings<MsSqlSettings>(builder.Configuration);
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
    .AddContext(mssqlSettings);

var app = builder.Build();

app.UseFileExceptionHandler();

app
    .AddKeepAliveEndpoint()
    .AddCropEndpoints()
    .AddCustomerEndpoints()
    .AddFieldsEndpoints()
    .AddHarvestEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
