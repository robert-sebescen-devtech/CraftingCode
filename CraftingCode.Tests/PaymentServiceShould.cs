using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace CraftingCode.Tests
{
    public class PaymentServiceShould
    {
        [Theory, AutoMoqData]
        public void ThrowAnExceptionIfUserIsInvalid(User invalidUser, Mock<IUserService> mockedUserService, Mock<IPaymentGateway> mockedPaymentGateway)
        {
            mockedUserService.Setup(u => u.HasValidAccount(invalidUser)).Returns(false);
            var paymentService = new PaymentService(mockedUserService.Object, mockedPaymentGateway.Object);
            Assert.Throws<Exception>(() => paymentService.ProcessPayment(invalidUser, null));
        }

        [Theory, AutoMoqData]
        public void ProcessPaymentDetailsWhenUserIsValid(User validUser, Mock<IUserService> mockedUserService, Mock<IPaymentGateway> mockedGateway, PaymentDetails paymentDetails)
        {
            mockedUserService.Setup(u => u.HasValidAccount(validUser)).Returns(true);
            var paymentService = new PaymentService(mockedUserService.Object, mockedGateway.Object);
            paymentService.ProcessPayment(validUser, paymentDetails);
            mockedGateway.Verify(g => g.PayWith(paymentDetails), Times.Once);
        }
    }
}
