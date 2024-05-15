using ApplicationForm.Core.Model;
using Microsoft.Azure.Cosmos;

namespace ApplicationForm.Manager
{
    public class ProgramApplicationManager : IProgramApplicationManager
    {

        private readonly Container _container;

        public ProgramApplicationManager(CosmosClient cosmosClient)
        {
            _container = cosmosClient.GetContainer("ApplicatioForm", "Program");
        }

        public async Task<string> CreateProgram(ProgramApplication application)
        {
               await _container.CreateItemAsync(application,
               new PartitionKey(application.Id)).ConfigureAwait(false);
               return application.Id;
        }

        public async Task<string> UpdateProgram(ProgramApplication application, string programId)
        {
            await _container.ReplaceItemAsync(application,programId,new PartitionKey(application.Id)).ConfigureAwait(false);
            return application.Id;
        }

        public async Task<ProgramApplication> GetProgramApplicationById(string programId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @programId")
        .WithParameter("@programId", programId);

            var iterator = _container.GetItemQueryIterator<ProgramApplication>(query);

            if (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync().ConfigureAwait(false);
                return response.FirstOrDefault();
            }

            return null;
        }
    }
}
