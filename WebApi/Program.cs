using System.Text;
using BusinessLogic;
using BusinessLogic.Auth;
using DotNetEnv;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

builder.Services.AddSingleton(Options.Create(jwt));

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
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanCreateMovies", p => p.RequireClaim("permission", "movie:create"));
    //TODO: add remaining policies after discussion
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher<EUser>, PasswordHasher<EUser>>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapGraphQL();
app.Run();
