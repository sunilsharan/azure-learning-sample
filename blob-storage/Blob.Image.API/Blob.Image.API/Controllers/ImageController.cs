using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Blob.Image.API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.IO;

namespace Blob.Image.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private ConfigOption _ConfigOption;

        public ImageController(ConfigOption configOption)
        {
            _ConfigOption = configOption;
        }

        private async Task<BlobContainerClient> GetContainerClient(string containerName)
        {
            BlobServiceClient client = new BlobServiceClient(_ConfigOption.ConnectionString);
            BlobContainerClient containerClient = client.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            return containerClient;
        }   

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            BlobContainerClient client = await GetContainerClient(_ConfigOption.Container);
            List<string> result = new List<string>();

            await foreach (BlobItem item in client.GetBlobsAsync())
            {
                result.Add(Flurl.Url.Combine(
                    client.Uri.AbsoluteUri,
                    item.Name
                    ));
            }
            return result;
        }
       // [Route("/")]
        [HttpPost]
        public async Task<ActionResult> Post()
        {
            Stream image = Request.Body;
            BlobContainerClient containerClient = await GetContainerClient(_ConfigOption.Container);
            string blobName = Guid.NewGuid().ToString().ToLower().Replace("-", String.Empty);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(image);
            return Created(blobClient.Uri, null);
        }

        

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


    }
}
