using Grpc.Core;
using ITIMenofia.Grpc.Data;
using ITIMenofia.Grpc.Protos;
using static ITIMenofia.Grpc.Protos.InventoryService;

namespace ITIMenofia.Grpc.Services
{
    public class InventoryService: InventoryServiceBase
    {
        private static List<Product> products = new List<Product>();
        static InventoryService()
        {
            products.Add(new Product { Id = "1", Quantity = 20 });
            products.Add(new Product { Id = "2", Quantity = 30 });
            products.Add(new Product { Id = "3", Quantity = 40 });
            products.Add(new Product { Id = "4", Quantity = 50 });
        }
         
         public override Task<CheckAvailabilityResponse> CheckAvailability(CheckAvailabilityRequest request, ServerCallContext context)
        {
            bool available = true;
            string message = "";
            var product = products.FirstOrDefault(p => p.Id ==request.Id);


            if (product != null && product.Quantity >= request.Quantity)
            {
                product.Quantity  -= product.Quantity;
                available = true;
                message = "Product Done";

            }
            else if (product == null)
            {
                available = false;
                message = "Product Not Found";
            }
            else
            {
                available = false;
                message = "Out of Stock";
            }
         
            var response = new CheckAvailabilityResponse
            {
                Available = available,
                Message  = message
            };

            return Task.FromResult(response);
        }
    }
}






