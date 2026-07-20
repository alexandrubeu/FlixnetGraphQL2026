using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMoviesService, MoviesService>();
builder.Services.AddSingleton<IGenresService, GenresService>();
builder.Services.AddSingleton<ICollectionsService, CollectionsService>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutations>()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapGraphQL();
app.Run();
