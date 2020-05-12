using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Profile.Service
{
    public interface IUserProfileService
    {
        Task<Model.User> GetUser(string userID, string partitionKey);

        Task<IEnumerable<Model.User>> GetUsers();

        Task CreateUser(Model.User user);

        Task DeleteUser(string userId);

        Task<Model.User> UpdateUser(Model.User user);
    }
}
