using Microsoft.AspNetCore.Mvc;
using PayPalGateway.DbCtx;
using PayPalGateway.Models;
using PayPalGateway.Services;
using System;
using System.Threading.Tasks;

namespace PayPalGateway.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PaymentService _paymentService;
        
        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(string amount)
        {
            try
            {
                var paymentUrl = await _paymentService.CreatePayPalPaymentAsync(amount);
                return Redirect(paymentUrl);
            }
            catch (Exception ex)
            {
                // Handle errors appropriately
                return BadRequest($"Payment processing failed: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Success(string paymentId, string token, string payerId)
        {
            try
            {
                await _paymentService.StorePaymentDetailsAsync(paymentId, token, payerId);
                // Optionally, you can redirect the user to a thank you page or perform other actions
                return View("Success"); // Assuming you have a Success.cshtml view
            }
            catch (Exception ex)
            {
                // Handle errors appropriately
                return BadRequest($"Failed to store payment details: {ex.Message}");
            }
        }

    }
}
