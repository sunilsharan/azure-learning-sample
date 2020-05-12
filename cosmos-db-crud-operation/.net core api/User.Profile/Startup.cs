using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using User.Profile.Service;

namespace User.Profile
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                Console.WriteLine("Ignoring SSL verification");
                return true;
            };

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IUserProfileService>(
                InitializeCosmosClientInstanceAsync(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();
            app.UseMvc();
          

        }

        // <InitializeCosmosClientInstanceAsync>        
        /// <summary>
        /// Creates a Cosmos DB database and a container with the specified partition key. 
        /// </summary>
        /// <returns></returns>
        private static async Task<UserProfileService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        {

            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(account, key);
           
            CosmosClient client = clientBuilder
                                .WithConnectionModeDirect()
                                .Build();
           
            UserProfileService userProfileService = new UserProfileService(client, databaseName, containerName);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/address/postalcode");

            return userProfileService;
        }
    }
}
