using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using infrastructure.Api;
using Microsoft.OpenApi.Models;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

IConfiguration conf = builder.Configuration;

builder.WebHost.UseUrls(conf["UseUrls"]);

builder.Services.AddControllers(config =>
{
    config.Filters.Add<ApiExceptionFilterAttribute>();
});

// Add services to the container.
builder.Services.RegisterServices(builder.Configuration);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
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


app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI();

/*

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

*/


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
