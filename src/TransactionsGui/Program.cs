using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TransactionsGraphQLClient;
using TransactionsGui;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

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

await builder.Build().RunAsync();

