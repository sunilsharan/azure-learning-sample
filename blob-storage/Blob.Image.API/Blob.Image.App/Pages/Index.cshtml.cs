using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blob.Image.App.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Blob.Image.App.Pages
{
    public class IndexModel : PageModel
    {
        private HttpClient _HttpClient;
        private ConfigOption _ConfigOption;

        public IndexModel(HttpClient httpClient, ConfigOption configOption)
        {
            _HttpClient = httpClient;
            _ConfigOption = configOption;
        }
        [BindProperty]
        public List<string> ImageList { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task OnGetAsync()
        {
            var imagesUrl = _ConfigOption.ApiUrl;
            string imagesJson = await _HttpClient.GetStringAsync(imagesUrl);
            IEnumerable<string> imagesList = JsonConvert.DeserializeObject<IEnumerable<string>>(imagesJson);
            this.ImageList = imagesList.ToList<string>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Upload != null && Upload.Length > 0)
            {
                var imagesUrl = _ConfigOption.ApiUrl;

                using (var image = new StreamContent(Upload.OpenReadStream()))
                {
                    image.Headers.ContentType = new MediaTypeHeaderValue(Upload.ContentType);
                    var response = await _HttpClient.PostAsync(imagesUrl, image);
                }
            }
            return RedirectToPage("/Index");
        }
    }
}
