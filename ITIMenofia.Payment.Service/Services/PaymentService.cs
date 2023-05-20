using Grpc.Core;
using ITIMenofia.Payment.Service.Data;
using ITIMenofia.Payment.Service.Protos;
using static ITIMenofia.Payment.Service.Protos.PaymentService;

namespace ITIMenofia.Payment.Service.Services
{
    public class PaymentService: PaymentServiceBase
    {
        private static List<User> users = new List<User>();

        static PaymentService()
        {
            users.Add(new User { Id = "1", Balance = 1000 });
            users.Add(new User { Id = "2", Balance = 2000 });
            users.Add(new User { Id = "3", Balance = 3000 });
            users.Add(new User { Id = "4", Balance = 4000 });
        }
        public override Task<PaymentResponse> ProcessPayment(PaymentRequest request, ServerCallContext context)
        {
            bool success;
            string message;

            User user = users.FirstOrDefault(u => u.Id == request.Id);
         
            if (user != null)
            {
                if (user.Balance >= request.Amount)
                {
                    user.Balance -= request.Amount;
                    success = true;
                    message = "Payment Done";
                }
                else
                {
                    success = false;
                    message = "No Money";
                }
            }
            else
            {
                success = false;
                message = "User not found";
            }

            var response = new PaymentResponse
            {
                Success = success,
                Message = message
            };

            return Task.FromResult(response);
        }
    }
}
