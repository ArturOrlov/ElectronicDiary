using System.Text;
using ElectronicDiary.AutoMapper;
using ElectronicDiary.Bootstrap;
using ElectronicDiary.Context;
using ElectronicDiary.Entities.DbModels;
using ElectronicDiary.Extension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddAutoMapper(typeof(AutoMappingProfile));

builder.Services.AddDbContext<ElectronicDiaryDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, Role>(identityOptions =>
    {
        identityOptions.Password.RequiredLength = 3;
        identityOptions.Password.RequireUppercase = false;
        identityOptions.Password.RequireLowercase = false;
        identityOptions.Password.RequireNonAlphanumeric = false;
        identityOptions.User.RequireUniqueEmail = false;
        identityOptions.Password.RequireDigit = false;
    })
    .AddEntityFrameworkStores<ElectronicDiaryDbContext>()
    .AddDefaultTokenProviders();;

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = configuration["Jwt:ValidIssuer"],
            ValidAudience = configuration["Jwt:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]))
        };
    });

builder.Services.AddCustomServices(); // подсмотерл с автобонусов
builder.Services.AddCustomRepository(); // подсмотерл с автобонусов

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "ElectronicDiary Api",
        Description = "Dot Net Api"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
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

builder.Services.ConfigureSwaggerGen(options =>
{
    options.CustomSchemaIds(x => x.FullName);
}); // подсмотерл с автобонусов

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var serviceScope = app.Services.CreateScope();
var services = serviceScope.ServiceProvider;
var bootstrap = services.GetRequiredService<Bootstrap>();
await bootstrap.SeedDb();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Autobonus backend API"); });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();