using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Ester.FarmetTracker.UI.Web.ActionFilters;
using Alp.ServiceExtensions;
using Ester.FarmetTracker.UI.Web.Infrastructures._settings;
using Ester.FarmetTracker.Common.Settings;
using Ester.FarmetTracker.UI.Web.Middlewares;
using Ester.FarmetTracker.UI.Web.Infrastructures._extensions;
using Ester.FarmetTracker.UI.Web.Infrastructures.KeepAlives.Alive;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<FillTokenParameterAttribute>();
}).AddRazorRuntimeCompilation();

builder.Services.AddSingleton<IDataProtectionProvider>(s => DataProtectionProvider.Create("FarmerTracker"));

builder.Services.Configure<CookieTempDataProviderOptions>(options =>
{

});
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

var externalServiceEndpoints = builder.Services.AddSettings<ExternalServiceEndpoints>(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenParameters>();

builder.Services
       .AddCustomExceptionHandler(builder.Environment)
       .AddHttpClients(externalServiceEndpoints);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.AddKeepAliveEndpoint();



app.UseFileWebExceptionHandler();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
