using Microsoft.Extensions.Configuration;
using TransactionsCore.Interfaces;

namespace TransactionsData
{
    public static class DependencyInjection
    {
        public static ITransactionsRepository InitializeCosmosClientInstance(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            ITransactionsRepository cosmosDbService = new TransactionsRepository(client, databaseName, containerName);
            Microsoft.Azure.Cosmos.DatabaseResponse database = client.CreateDatabaseIfNotExistsAsync(databaseName).GetAwaiter().GetResult();
            database.Database.CreateContainerIfNotExistsAsync(containerName, "/id").GetAwaiter().GetResult();

            return cosmosDbService;
        }
    }
}
