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
using ChatAppServer.Services.IServices;
using ChatAppServer.Services;
using ChatAppServer.Schema.Subcriptions;
using HotChocolate.AspNetCore.Subscriptions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<MongoSetting>(builder.Configuration.GetSection("MongoSettings"));
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

#region mongo identity
// 
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

#endregion

#region auth
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

builder.Services.AddAuthorization(o =>
{
    
});

#endregion

// graphql server
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
        .AddTypeExtension<UserQuery>()
        .AddTypeExtension<AuthQuery>()
        .AddTypeExtension<GroupQuery>()
        .AddTypeExtension<MessageQuery>()
    .AddMutationType<Mutation>()
        .AddTypeExtension<UserMutation>()
        .AddTypeExtension<GroupMutation>()
        .AddTypeExtension<MessageMutation>()
    .AddType<UserType>()
    .AddType<ResultType>()
    .AddType<GroupType>()
    .AddAuthorization()
    .AddFluentValidation();

builder.Services.AddTransient<RegisterInputTypeValidator>();

#region repose and services

// repos
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

// services
builder.Services.AddTransient<IJwtService, JwtService>();


#endregion

// enable cors
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// enable cors
app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

//app.MapGet("/", () => "Hello World!");
app.MapGraphQL();
app.UseWebSockets();

app.Run();
