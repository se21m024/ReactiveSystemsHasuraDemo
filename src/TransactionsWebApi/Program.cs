using TransactionsCore.Interfaces;
using TransactionsData;
using TransactionsGraphQL.Resolvers;

var builder = WebApplication.CreateBuilder(args);

const string corsPolicy = "AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        x =>
        {
            x.AllowAnyOrigin();
            x.AllowAnyHeader();
            x.AllowAnyMethod();
        });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// In Memory
builder.Services.AddSingleton<ITransactionsRepository, TransactionsRepositoryInMemory>();

// CosmosDb
//builder.Services.AddSingleton<ITransactionsRepository>(
//    DependencyInjection.InitializeCosmosClientInstance(builder.Configuration.GetSection("CosmosDb")));

builder.Services.AddGraphQLServer().AddQueryType<Query>();

var app = builder.Build();

app.UseRouting();

app.UseCors(corsPolicy);

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGraphQL();
});
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
