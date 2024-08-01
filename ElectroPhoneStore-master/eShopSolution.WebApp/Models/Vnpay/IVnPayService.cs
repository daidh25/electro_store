
using Microsoft.AspNetCore.Http;

namespace eShopSolution.WebApp.Models.Vnpay
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
