using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingCode
{
    public class PaymentService
    {
        private readonly IUserService _userService;
        private readonly IPaymentGateway _paymentGateway;

        public PaymentService(IUserService userService, IPaymentGateway paymentGateway)
        {
            _userService = userService;
            _paymentGateway = paymentGateway;
        }

        public void ProcessPayment(User user, PaymentDetails paymentDetails)
        {
            if (!_userService.HasValidAccount(user))
            {
                throw new Exception();
            }

            _paymentGateway.PayWith(paymentDetails);
        }
    }

    public class PaymentDetails
    {
    }

    public class PaymentGateway : IPaymentGateway
    {
        public void PayWith(PaymentDetails paymentDetails)
        {
            throw new NotImplementedException();
        }
    }

    public class UserService : IUserService
    {
        public bool HasValidAccount(User user)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUserService
    {
        bool HasValidAccount(User user);
    }

    public class User
    {
        
    }
}
