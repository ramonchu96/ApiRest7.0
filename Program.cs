using Api_Ayanet_2.Data;
using Api_Ayanet_2.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using AutoMapper;
using Api_Ayanet_2.Mappers;
using Api_Ayanet_2.Repositories.IRepository;
using Api_Ayanet_2.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSql"));
});

var key = builder.Configuration.GetValue<string>("AppSettings:SecretKey");

builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding repositories
builder.Services.AddScoped<IProductsRepositories, ProductsRepository>();
builder.Services.AddScoped<IClientesRepositories, ClientesRepository>();
builder.Services.AddScoped<IUsuarioRepositories, UserRepository>();

//Add AutoMapper Dependency Injection
builder.Services.AddAutoMapper(typeof(Mapper_Ayanet));

//Add Autehntication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudence = false
    };
});

var app = builder.Build();

app.UseCors(cors => cors.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());


// Swagger
app.UseSwagger();
app.UseSwaggerUI(config => config.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object> { ["activated"] = false });

//app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();