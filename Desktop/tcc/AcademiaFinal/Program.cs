using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NexusGym.Application.Mappings;
using NexusGym.Application.Validators;
using NexusGym.Domain.Abstractions.Clientes;
using NexusGym.Domain.Abstractions.Equipamentos;
using NexusGym.Domain.Abstractions.Planos;
using NexusGym.Domain.Abstractions.Usuarios;
using NexusGym.Infrastructure.Data;
using NexusGym.Infrastructure.Repositories;
using NexusGym.Application.Services;
using NexusGym.Application.Services.NovaPasta;
using NexusGym.Application.Services.Security;
using Scrutor;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using NexusGym.Application.Mapping;
using NexusGym.Application.Services.UseCases.Equipamentos;
using NexusGym.Application.Services.UseCases.Planos;
using NexusGym.Application.Validators.Cliente;
using NexusGym.Application.Validators.Equipamento;
using NexusGym.Infrastructure.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// ================= JWT SETTINGS =================
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

// ================= JWT AUTH =================
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSection["Key"]!);

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
        ValidIssuer = jwtSection["Issuer"],

        ValidateAudience = true,
        ValidAudience = jwtSection["Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// ================= CORS =================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ================= CONTROLLERS =================
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ================= DB CONTEXT =================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ================= REPOSITORIES =================
builder.Services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
builder.Services.AddScoped<IPlanoRepository, PlanoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<FichaDeTreinoRepository>();
builder.Services.AddScoped<PlanoClienteRepository>();


// ================= TOKEN SERVICE =================
builder.Services.AddScoped<IToken, GerarTokenJWT>();

// ================= SERVICES =================
builder.Services.AddScoped<FichaDeTreinoService>();
builder.Services.AddScoped<PlanoClienteService>();

// ================= AUTOMAPPER =================
builder.Services.AddAutoMapper(typeof(PlanoProfile).Assembly);
builder.Services.AddAutoMapper(typeof(EquipamentoProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ClienteProfile).Assembly);
builder.Services.AddAutoMapper(typeof(UsuarioProfile).Assembly);
builder.Services.AddAutoMapper(typeof(FichaDeTreinoProfile).Assembly);
builder.Services.AddAutoMapper(typeof(PlanoClienteProfile).Assembly);

// ================= VALIDATORS =================
builder.Services.AddValidatorsFromAssemblyContaining<PlanoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EquipamentoCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ClienteCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PlanoClienteValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddHttpContextAccessor();

// ================= USE CASES (SCAN) =================
builder.Services.Scan(scan => scan
    .FromAssemblies(Assembly.GetExecutingAssembly())
    .AddClasses(classes => classes.Where(c => c.Name.EndsWith("UseCase")))
    .AsSelf()
    .WithScopedLifetime()
);

// ================= FACADE =================
builder.Services.AddScoped<EquipamentoFacade>();
builder.Services.AddScoped<PlanosFacade>();

var app = builder.Build();

// ================= PIPELINE =================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
