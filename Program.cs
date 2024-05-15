
using ApplicationForm.Helper;
using ApplicationForm.Manager;
using Microsoft.Azure.Cosmos;

namespace ApplicationForm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var configutation = builder.Configuration;

            builder.Services.AddSingleton((provider) =>
            {
                var endpointUri = configutation["CosmosDbSettings:EndpointUri"];
                var primaryKey = configutation["CosmosDbSettings:PrimaryKey"];
                var databaseName = configutation["CosmosDbSettings:DatabaseName"];
                var cosmosClientOptions = new CosmosClientOptions
                {
                    ApplicationName = databaseName
                };

                var cosmosClient = new CosmosClient(endpointUri, primaryKey, cosmosClientOptions);
                cosmosClient.ClientOptions.ConnectionMode = ConnectionMode.Direct;
                return cosmosClient;
            });

            builder.Services.AddScoped<IProgramApplicationManager,ProgramApplicationManager> ();
            builder.Services.AddScoped<IProgramApplicationHelper,ProgramApplicationHelper> ();
            builder.Services.AddScoped<IResponseManager,ResponseManager> ();
            builder.Services.AddScoped<IResponseHelper,ResponseHelper> ();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
