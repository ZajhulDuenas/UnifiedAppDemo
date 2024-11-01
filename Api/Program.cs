using infrastructure.Api;
using Microsoft.OpenApi.Models;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Llama a tu método de extensión aquí
builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddControllers(config =>
{
    
    config.Filters.Add<ApiExceptionFilterAttribute>();
});


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("x-token", new OpenApiSecurityScheme
    {
        Name = "x-token",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "x-token",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the x-token scheme."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "x-token"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
});


builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
