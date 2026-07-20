using System.Text;
using BusinessLogic;
using BusinessLogic.Auth;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
//using WebApi.GraphQL.Services;
using Microsoft.IdentityModel.Tokens;
using Repository;
using WebApi.GraphQL.Mutations;
using WebApi.GraphQL.Queries;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Default")!)
);

builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenresService, GenresService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMoviesService, MoviesService>();

//builder.Services.AddSingleton<ICollectionsService, CollectionsService>();

builder
    .Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutations>()
    .AddFiltering()
    .AddSorting();

// JWT Configuration
var jwt = new JwtSettings
{
    Key =
        Environment.GetEnvironmentVariable("JWT_SECRET")
        ?? throw new Exception("JWT_SECRET missing"),

    Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "MovieAPI",

    Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "MovieAPI-Client",

    ExpiryMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES") ?? "60"),
};
builder.Services.Configure<JwtSettings>(options =>
{
    options.Key = jwt.Key;
    options.Issuer = jwt.Issuer;
    options.Audience = jwt.Audience;
    options.ExpiryMinutes = jwt.ExpiryMinutes;
});

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt.Issuer,
            ValidateAudience = true,
            ValidAudience = jwt.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
        };
    });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapGraphQL();
app.Run();
