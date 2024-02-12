using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using ChatAppServer.Models;
using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ChatAppServer.Schema.Types;
using ChatAppServer.Schema.Queries;

var builder = WebApplication.CreateBuilder(args);

BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

// mongo setting servics
builder.Services.Configure<MongoDbSetting>(builder.Configuration.GetSection("MongoSettings"));
//builder.Services.AddSingleton<MongoDbSetting>(builder.Configuration.GetSection("MongoSettings").Get<MongoDbSetting>()!);

// graphql
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
        .AddTypeExtension<UserQuery>()
        .AddTypeExtension<GroupQuery>()
        .AddTypeExtension<MessageQuery>()
    .AddType<UserType>()
    .AddType<MessageType>()
    .AddType<GroupType>()
    ;

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
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireLowercase = true;

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
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
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


builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});



var app = builder.Build();

app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


//app.MapGet("/", () => "Hello World!");
app.MapGraphQL();
app.UseWebSockets();

app.Run();
