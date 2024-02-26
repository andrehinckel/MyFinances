using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
    options.TokenValidationParameters =
        new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidAudience = builder.Configuration["Auth0:Audience"],
            ValidIssuer = $"{builder.Configuration["Auth0:Domain"]}",
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true
        };
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Finances", Version = "v1" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "Using the Authorization header with the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    options.AddSecurityDefinition("Bearer", securitySchema);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securitySchema,
            Array.Empty<string>()
        }
    });
});

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

app.Run();