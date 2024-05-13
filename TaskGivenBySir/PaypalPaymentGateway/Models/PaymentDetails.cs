﻿using System.ComponentModel.DataAnnotations;

namespace PayPalGateway.Models
{
    public class PaymentDetails
    {
        [Key]
        public string PaymentId { get; set; }
        public string Token { get; set; }
        public string PayerId { get; set; }
    }

}
