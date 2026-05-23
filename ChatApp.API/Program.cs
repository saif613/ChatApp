using ChatApp.API.Hubs;
using ChatApp.API.Middlewares;
using ChatApp.API.Services;
using ChatApp.Application.ChatApp.Application;
using ChatApp.Application.Interface.Service;
using ChatApp.Application.Interfaces.Service;
using ChatApp.Application.Interfaces.Services;
using ChatApp.Application.Mapping;
using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Identity;
using ChatApp.Infrastructure.Persistence;
using ChatApp.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Database

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlServer(connectionString));


// Identity

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ChatDbContext>()
    .AddDefaultTokenProviders();


// Services

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ITokenService, JwtTokenService>();

builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddScoped<IChatNotifier, SignalRChatNotifier>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IConversationService, ConversationService>();

builder.Services.AddScoped<IUserConnectionService, UserConnectionService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();


builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);


// MediatR

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(ApplicationAssemblyReference).Assembly);
});


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MessageProfile>();
}, AppDomain.CurrentDomain.GetAssemblies());


// JWT Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,

            ValidateAudience = true,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,

            ValidIssuer =
                builder.Configuration["Jwt:Issuer"],

            ValidAudience =
                builder.Configuration["Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:Key"]!))
        };

    // SignalR JWT Support

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken =
                context.Request.Query["access_token"];

            var path =
                context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken)
                && path.StartsWithSegments("/chatHub"))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});


// Authorization

builder.Services.AddAuthorization();


builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowReact",
        policy =>
        {
            policy
                .WithOrigins(
    "http://localhost:5173")

                .AllowAnyHeader()
                .AllowAnyMethod()

                .AllowCredentials();
        });
});

builder.Services.AddControllers();


// Swagger / OpenAPI
builder.Services.AddSwaggerGen(options =>
{

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Scheme = "bearer",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token as: *Bearer [your_token]*",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

var app = builder.Build();




// Swagger

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


// HTTPS

app.UseHttpsRedirection();


app.UseCors("AllowReact");

app.UseMiddleware<ExceptionMiddleware>();

app.UseMiddleware<RequestLoggingMiddleware>();
// Authentication

app.UseAuthentication();


// Authorization

app.UseAuthorization();


// Controllers

app.MapControllers();


// SignalR Hub

app.MapHub<ChatHub>("/chatHub");

using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

    await IdentitySeeder
        .SeedRolesAsync(roleManager);
}

app.Run();