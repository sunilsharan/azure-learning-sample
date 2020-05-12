using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Profile.Service
{
    public class UserProfileService:IUserProfileService
    {
        private Container _container;

        public UserProfileService(CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            _container = this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task CreateUser(Model.User user)
        {
            await _container.CreateItemAsync(user, new PartitionKey(user.Address.PostalCode));
        }

        public async Task DeleteUser(string userId)
        {
            string query = $"SELECT c.id, c.address.postalcode FROM c where c.id =  '{ userId }'";

            var iterator = _container.GetItemQueryIterator<dynamic>(query);
            var documents = (await iterator.ReadNextAsync()).ToList();
            foreach (var item in documents)
            {
                string id = item.id;
                string pk = item.postalcode;
                await _container.DeleteItemAsync<dynamic>(id, new PartitionKey(pk));
            }
        }

        public async Task<Model.User> GetUser(string userId, string partitionKey)
        {
            string query = $"SELECT* FROM c where c.id = '{ userId }'";
            //string query = $"SELECT c.id, c.address.postalcode FROM c where c.id =  '{ userId }'";

            var iterator = _container.GetItemQueryIterator<Model.User>(query);
            var documents = (await iterator.ReadNextAsync()).ToList();
            if(documents.Count==1)
            {
                return documents[0];
            }
            else { return null; }
           
        }

        public async Task<IEnumerable<Model.User>> GetUsers()
        {
            //Select all items from container
            var query = this._container.GetItemQueryIterator<Model.User>(new QueryDefinition("SELECT * FROM c"));
            List<Model.User> results = new List<Model.User>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<Model.User> UpdateUser(Model.User user)
        {
           return  await _container.ReplaceItemAsync(user, user.Id, new PartitionKey(user.Address.PostalCode));
        }
    }
}
