using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class ProductCreate
    {
        [FunctionName("ProductCreate")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products")] Product product,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (product == null || product.Name == null)
            {
                return new BadRequestObjectResult("Product missing");
            }

            var newProduct = Db.CreateProduct(product);
            return new CreatedResult($"/api/products/{newProduct.Id}", newProduct);
        }
    }
}
