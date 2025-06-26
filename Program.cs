using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NexusGym.Application.Mapping;
using NexusGym.Application.Mappings;
using NexusGym.Application.Services;
using NexusGym.Application.Validators;
using NexusGym.Infrastructure.Data;
using NexusGym.Infrastructure.Repositories;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],

        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<EquipamentoRepository, EquipamentoRepository>();
builder.Services.AddScoped<EquipamentoService>();

builder.Services.AddScoped<PlanoRepository, PlanoRepository>();
builder.Services.AddScoped<PlanoService>();

builder.Services.AddScoped<ClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ClienteService>();

builder.Services.AddScoped<UsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<FichaDeTreinoRepository, FichaDeTreinoRepository>();
builder.Services.AddScoped<FichaDeTreinoService>();

builder.Services.AddScoped<PlanoClienteRepository, PlanoClienteRepository>();
builder.Services.AddScoped<PlanoClienteService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(PlanoProfile).Assembly);
builder.Services.AddAutoMapper(typeof(EquipamentoProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ClienteProfile).Assembly);
builder.Services.AddAutoMapper(typeof(UsuarioProfile).Assembly);
builder.Services.AddAutoMapper(typeof(FichaDeTreinoProfile).Assembly);
builder.Services.AddAutoMapper(typeof(PlanoClienteProfile).Assembly);

// VALIDADORES
builder.Services.AddValidatorsFromAssemblyContaining<PlanoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EquipamentoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ClienteValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PlanoClienteValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");  

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
