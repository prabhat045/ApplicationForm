using ApplicationForm.Core.Model;
using Microsoft.Azure.Cosmos;

namespace ApplicationForm.Manager
{
    public class ResponseManager:IResponseManager
    {

        private readonly Container _container;
        public ResponseManager(CosmosClient cosmosClient, IConfiguration configuration) {
            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            var containerName = configuration["CosmosDbSettings:ResponseContainer"];
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task SubmitApplication(Response response)
        {
            await _container.CreateItemAsync(response,new PartitionKey(response.Id)).ConfigureAwait(false); 
        }

    }
}
