using ApplicationForm.Core.Model;
using Microsoft.Azure.Cosmos;

namespace ApplicationForm.Manager
{
    public class ResponseManager:IResponseManager
    {

        private readonly Container _container;
        public ResponseManager(CosmosClient cosmosClient) {
            _container = cosmosClient.GetContainer("ApplicatioForm", "Response");
        }

        public async Task SubmitApplication(Response response)
        {
            await _container.CreateItemAsync(response,new PartitionKey(response.Id)).ConfigureAwait(false); 
        }

    }
}
