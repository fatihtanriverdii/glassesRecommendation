using glassesRecommendation.Core.Interfaces;
using glassesRecommendation.Core.Settings;
using glassesRecommendation.Data.Context;
using glassesRecommendation.Data.Repositories;
using glassesRecommendation.Service.Mappers;
using glassesRecommendation.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var JwtSettings = builder.Configuration.GetSection("JwtSettings");

// Add services to the container.
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend",
		builder =>
		{
            builder
            .WithOrigins("http://192.168.1.111:3000", "http://localhost:3000", "https://glassify-frontend-791546846158.europe-west1.run.app")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
            //.SetIsOriginAllowed(origin => true);
		});
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JwtSettings["Issuer"],
        ValidAudience = JwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JwtSettings["Key"]))
    };
});

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGlassesRepository, GlassesRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGlassesService, GlassesService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
