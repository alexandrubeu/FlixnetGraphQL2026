using System.Text;
using BusinessLogic;
using BusinessLogic.Auth;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using WebApi.Auth;
using WebApi.GraphQL.Mutations;
using WebApi.GraphQL.Queries;

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
    .AddTypeExtension<AuthMutation>()
    .AddAuthorization()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .ModifyRequestOptions(o => { o.IncludeExceptionDetails = true; });


/* OLD
// JWT Configuration
var jwt = new JwtSettings
{
    Key =
        Environment.GetEnvironmentVariable("JWT_SECRET")
        ?? throw new Exception("JWT_SECRET missing"),
    Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "MovieAPI",
    Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "MovieAPI",
    ExpiryMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES") ?? "60"),
};
builder.Services.AddSingleton(Options.Create(jwt));
*/

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
          ?? throw new Exception("[AUTH][JWT] appsettings.json is missing the Jwt section. ");

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

// Generate Policies based on permissions from /Auth/Permissions.cs
builder.Services.AddAuthorization(options =>
{
    foreach (var permission in Permissions.All)
    {
        options.AddPolicy(permission, policy => policy.RequireClaim("permission", permission));
    }
});

builder
    .Services.AddIdentityCore<EUser>(options =>
    {
        //sample password requirements
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 10;
    })
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
    await RolePermissionSeeder.SeedRolePermissionsAsync(scope.ServiceProvider);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapGraphQL();
app.Run();
