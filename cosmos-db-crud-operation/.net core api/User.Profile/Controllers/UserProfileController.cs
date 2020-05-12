using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Profile.Service;

namespace User.Profile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private IUserProfileService _Service;

        public UserProfileController(IUserProfileService service)
        {
            _Service = service;
        }

        // GET: api/UserProfile
        [HttpGet]
        public async Task<IEnumerable<Model.User>> Get()
        {
            return await _Service.GetUsers();
        }

        // GET: api/UserProfile/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Model.User> Get(string id, string partitionKey)
        {
            return  await _Service.GetUser(id, partitionKey);
        }

        // POST: api/UserProfile
        [HttpPost]
        public async Task  Post([FromBody] Model.User  user)
        {
            var user1 = new Model.User
            {
                Id= Guid.NewGuid().ToString(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DOB = user.DOB,
                Address = user.Address
            };
            await _Service.CreateUser(user1);
        }

        // PUT: api/UserProfile/5
        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] Model.User user)
        {
            user.Id = id;
            await _Service.UpdateUser(user);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _Service.DeleteUser(id);
        }
    }
}