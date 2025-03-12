using IdentityServer.UI.Api.Middlewares;
using IdentityServer.Persistence;
using IdentityServer.Application;
using IdentityServer.UI.Api.KeepAlives.Alive;
using IdentityServer.UI.Api.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<AuthorizeHandlerAttribute>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//Exception Yönetimini Saðlar.
app.UseExceptionHandlerMiddleware();

app.AddKeepAliveEndpoint();



app.UseAuthorization();

app.MapControllers();

app.Run();
