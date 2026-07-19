using Microsoft.EntityFrameworkCore;
using WebApi.Mutation;

var builder = WebApplication.CreateBuilder(args);

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
app.Run();