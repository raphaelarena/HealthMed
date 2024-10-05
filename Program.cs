using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HealthMed.Infrastructure.Security;
using HealthMed.Application.Interfaces;
using HealthMed.Infrastructure.Data;
using HealthMed.Infrastructure.Repositories;
using HealthMed.Application.Services;
using Microsoft.OpenApi.Models;
using HealthMed.Infrastructure.Auth;
using HealthMed.Infrastructure.Email;
using HealthMed.Infrastructure.Interface;
using HealthMed.Middleware;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using HealthMed.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HealthMedDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthMed API", Version = "v1" });

    // Configuração para adicionar suporte a autenticação Bearer Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT nesta caixa. Exemplo: Bearer {seu_token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
           Array.Empty<string>()
        }
    });
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddEndpointsApiExplorer();
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Secret"));

var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
builder.Services.AddSingleton(emailSettings);

builder.Services.AddAuthentication(
    x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
   ).AddJwtBearer(x =>
   {
       x.RequireHttpsMetadata = false;
       x.SaveToken = true;
       x.TokenValidationParameters = new TokenValidationParameters()
       {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(key),
           ValidateIssuer = false,
           ValidateAudience = false
       };
   });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:7287")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
