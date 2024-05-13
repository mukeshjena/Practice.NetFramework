using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PayPalGateway.Models;

namespace PayPalGateway.Controllers
{
    public class GetDetailsController : Controller
    {
        private readonly HttpClient _httpClient;
        public GetDetailsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetPaymentDetails(string paymentId)
        {
            var accessToken = "A21AALwiiSjuazQKCoAYZjAoxk_dSux5o1i0KJ-SuDCcyo-_TQVfYMgBvoJSfK5ZIL6DlC-wSDp_lPV1qw6pYXhKCFB_SKC8w"; // Replace with your access token
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"https://api.sandbox.paypal.com/v1/payments/payment/{paymentId}");
            if (response.IsSuccessStatusCode)
            {
                var paymentDetailsJson = await response.Content.ReadAsStringAsync();
                var paymentDetails = JsonConvert.DeserializeObject<PayPalPaymentDetails>(paymentDetailsJson);

                return View("PaymentDetails", paymentDetails);
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }
    }
}
