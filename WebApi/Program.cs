using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddGraphQLServer()
    .AddQueryType<DbLoggerCategory.Query>()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapGraphQL();
app.Run();
