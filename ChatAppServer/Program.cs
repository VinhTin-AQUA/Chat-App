using ChatAppServer.Data;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using ChatAppServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ChatAppServer.Schema.Types;
using ChatAppServer.Schema.Queries;
using ChatAppServer.Schema.Mutations;
using ChatAppServer.Repositories.IRepositories;
using ChatAppServer.Repositories;
using AppAny.HotChocolate.FluentValidation;
using ChatAppServer.Schema.Validators;
using Microsoft.AspNetCore.Mvc;
using ChatAppServer.Services.IServices;
using ChatAppServer.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<MongoSetting>(builder.Configuration.GetSection("MongoSettings"));
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

// mongo db
var mongoIdentityConfigs = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new MongoDbSettings
    {
        ConnectionString = builder.Configuration.GetSection("MongoSettings:ConnectionUri").Value,
        DatabaseName = builder.Configuration.GetSection("MongoSettings:DatabaseName").Value,
    },

    IdentityOptionsAction = (options) =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 3;

        options.User.RequireUniqueEmail = true;
    }
};

builder.Services.ConfigureMongoDbIdentity<AppUser, AppRole, Guid>(mongoIdentityConfigs)
    .AddSignInManager<SignInManager<AppUser>>()
    .AddUserManager<UserManager<AppUser>>()
    .AddRoleManager<RoleManager<AppRole>>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration.GetSection("JwtConfig:ValidIssuer").Value,
        ValidAudience = builder.Configuration.GetSection("JwtConfig:ValidAudience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtConfig:SecretKey").Value!)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
        .AddTypeExtension<UserQuery>()
    .AddMutationType<Mutation>()
        .AddTypeExtension<UserMutation>()
    .AddType<UserType>()
    .AddType<ResultType>()
    .AddFluentValidation();

builder.Services.AddTransient<RegisterInputTypeValidator>();

// repos
builder.Services.AddScoped<IUserRepository, UserRepository>();

// services
builder.Services.AddTransient<IJwtService, JwtService>();

// enable cors
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


var app = builder.Build();

// enable cors
app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


//app.MapGet("/", () => "Hello World!");
app.MapGraphQL();
app.UseWebSockets();

app.Run();
