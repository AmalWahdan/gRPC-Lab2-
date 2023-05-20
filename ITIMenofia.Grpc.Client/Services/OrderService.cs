
using Grpc.Core;
using Grpc.Net.Client;
using ITIMenofia.Grpc.Client.Protos;
using ITIMenofia.Grpc.Data;
using ITIMenofia.Grpc.Protos;
using ITIMenofia.Payment.Service.Data;
using ITIMenofia.Payment.Service.Protos;
using static ITIMenofia.Grpc.Client.Protos.OrderDetailsService;

namespace ITIMenofia.Grpc.Client.Services
{
    public class OrderServices : OrderDetailsServiceBase
    {

        public override  Task<OrderResponse> PostOrder(OrderRequest order, ServerCallContext context)
        {

            var InventoryChannel = GrpcChannel.ForAddress("http://localhost:5044");
            var InventoryClient = new InventoryService.InventoryServiceClient(InventoryChannel);
            var InventoryRequest = new CheckAvailabilityRequest() { Id = order.ProductId, Quantity = order.Quantity };
            var InventoryResponse = InventoryClient.CheckAvailability(InventoryRequest);

            var PaymentChannel = GrpcChannel.ForAddress("http://localhost:5227");
            var PaymentClient = new PaymentService.PaymentServiceClient(PaymentChannel);
            var PaymentRequest = new PaymentRequest() { Id = order.UserId, Amount = order.Amount };
            var PaymentResponse = PaymentClient.ProcessPayment(PaymentRequest);

            string msg = "";



            if (InventoryResponse.Available && PaymentResponse.Success)
            {
                msg = "Order has been created successfully (" + InventoryResponse.Message + " , " + PaymentResponse.Message + ")";
                return Task.FromResult(new OrderResponse() { Done = true, Message = msg });
            }
            else
            {
                msg = "Order Canceled Because " + (InventoryResponse.Available ? PaymentResponse.Message : InventoryResponse.Message);
                return Task.FromResult(new OrderResponse() { Done = false, Message = msg });

            }

        }
    }

}