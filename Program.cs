global using PlantsDetection.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PlantsDetection.Interfaces;
using PlantsDetection.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PlantsDetection.Identity;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<PlantsDetectionContext>();
builder.Services.AddDbContext<PlantsDetectionContext>(options => {

    options.UseSqlServer(builder.Configuration.GetConnectionString(name: "DefaultConnection"));

});


builder.Services.AddDbContext<AppIdentityDbContext>(options => {

    options.UseSqlServer(builder.Configuration.GetConnectionString(name: "IdentityConnection"));

});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<PlantsDetectionContext>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                ClockSkew = TimeSpan.Zero
            };
        });
// Configure the HTTP request pipeline.
//builder.Services.AddControllers();
// Register HttpClient
builder.Services.AddHttpClient("FlaskAPI", client =>
{
    client.BaseAddress = new Uri("http://127.0.0.1:5000"); // Replace with your actual Flask API URL
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<JwtService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
