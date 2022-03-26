using TransactionsGraphQLClient;
using TransactionsGui;

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

//builder.Services.AddScoped(sp =>
//    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
    .AddGraphQlClient()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri(Const.GraphQlHttpUri);
        client.DefaultRequestHeaders.Add(Const.AuthHeaderKey, Const.AuthHeaderValue);
    })

    // Not working because WebService endpoint of Hasura Cloud Instance cannot be found.
    .ConfigureWebSocketClient(client =>
    {
        client.Uri = new Uri(Const.GraphQlWsUri);
        client.ConnectionInterceptor = new CustomConnectionInterceptor();
    });

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

app.UseCors(corsPolicy);

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();


