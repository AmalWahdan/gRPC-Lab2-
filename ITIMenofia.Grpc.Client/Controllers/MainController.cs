using Grpc.Net.Client;
using ITIMenofia.Grpc.Client.Dtos;
using ITIMenofia.Grpc.Protos;
using ITIMenofia.Payment.Service.Protos;
using Microsoft.AspNetCore.Mvc;

namespace ITIMenofia.Grpc.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> orderProcess([FromBody] ProcessComponent order)
        {
            var InventoryChannel = GrpcChannel.ForAddress("http://localhost:5044");
            var InventoryClient = new InventoryService.InventoryServiceClient(InventoryChannel);
            var InventoryRequest = new CheckAvailabilityRequest() { Id = order.ProductId, Quantity = order.Quantity };
            var InventoryResponse = InventoryClient.CheckAvailability(InventoryRequest);

            var PaymentChannel = GrpcChannel.ForAddress("http://localhost:5227");
            var PaymentClient = new PaymentService.PaymentServiceClient(PaymentChannel);
            var PaymentRequest = new PaymentRequest() { Id = order.UserId, Amount = order.Amount };
            var PaymentResponse = PaymentClient.ProcessPayment(PaymentRequest);

    
            if (InventoryResponse.Available && PaymentResponse.Success)
            {
               
                return Ok("Order has been created successfully ("+InventoryResponse.Message+" , "+PaymentResponse.Message+")");
               
            }
            else 
            {
         
                return Ok( "Order Canceled Because " + (InventoryResponse.Available ? PaymentResponse.Message : InventoryResponse.Message));  
            }
         
      
        }
    }
}


