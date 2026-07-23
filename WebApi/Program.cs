using BusinessLogic;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Repository;
using WebApi.GraphQL.Mutations;
using WebApi.GraphQL.Queries;
//using WebApi.GraphQL.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Default")!));

builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenresService, GenresService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMoviesService, MoviesService>();

//builder.Services.AddSingleton<ICollectionsService, CollectionsService>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutations>()
    .AddProjections()
    .ModifyRequestOptions(o =>
    {
        o.IncludeExceptionDetails = true;
    });


var app = builder.Build();

app.UseHttpsRedirection();

app.MapGraphQL();

app.Run();
