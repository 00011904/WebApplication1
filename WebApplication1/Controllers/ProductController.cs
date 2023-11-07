using DSCC.CW1._11904_MVC.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DSCC.CW1._11904_MVC.Controllers
{
    public class ProductController : Controller
    {
        string BaseUrl = "http://ec2-13-48-105-115.eu-north-1.compute.amazonaws.com:80/";

        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            List<Product> ProdInfo = new List<Product>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                
                HttpResponseMessage Res = await client.GetAsync("api/Product");

                if (Res.IsSuccessStatusCode)
                {
                    var PrResponse = Res.Content.ReadAsStringAsync().Result;

                    ProdInfo = JsonConvert.DeserializeObject<List<Product>>(PrResponse);
                }
            }
            return View(ProdInfo);
        }
        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var product = new Product();

            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync($"api/Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<Product>(responseContent);
            }

            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product viewModel)
        {
            var product = new Product
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price
            };

            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var customerJson = JsonConvert.SerializeObject(product);
            var content = new StringContent(customerJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Product", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Redirect to the product list
            }
            else
            {
                return View("Error");
            }

            return View(viewModel);
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var product = new Product();

            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync($"api/Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<Product>(responseContent);
            }

            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product viewModel)
        {
            var product = new Product
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price
            };

            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Serialize the modified customer object to JSON and send it in the request body
            var customerJson = JsonConvert.SerializeObject(product);
            var content = new StringContent(customerJson, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/Product/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Redirect to the customer list or another appropriate action
            }

            // Handle the case where the update failed or ModelState is not valid
            return View(product);
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var product = new Product();

            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync($"api/Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<Product>(responseContent);
            }

            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.DeleteAsync($"api/Product/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Redirect to the order list or another appropriate action
            }
            else
            {
                return View("Error");
            }
        }
    }
}
