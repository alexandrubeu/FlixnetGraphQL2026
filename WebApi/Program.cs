using Application.MappingProfiles;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.GraphQL.Mutations;
using WebApi.GraphQL.Mutations.Collection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMapper>(_ =>
    new MapperConfiguration(cfg => cfg.AddMaps(typeof(CollectionProfile).Assembly)).CreateMapper());

builder.Services
    .AddGraphQLServer()
    .AddQueryType<DbLoggerCategory.Query>()
    .AddMutationType<AddCollection>()
    .AddFiltering()
    .AddSorting();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();
app.MapGraphQL();
app.Run();