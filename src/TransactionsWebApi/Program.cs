using TransactionsCore.Interfaces;
using TransactionsData;
using TransactionsGraphQL.Resolvers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITransactionsRepository>(
    DependencyInjection.InitializeCosmosClientInstance(builder.Configuration.GetSection("CosmosDb")));

builder.Services.AddGraphQLServer().AddQueryType<Query>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting().UseEndpoints(endpoints => { endpoints.MapGraphQL(); });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
