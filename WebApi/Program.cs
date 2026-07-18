using WebApi.Mutation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<AddCollection>()
    .AddFiltering()
    .AddSorting();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();