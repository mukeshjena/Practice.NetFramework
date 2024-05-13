using System;
using System.Collections.Generic;

namespace PayPalGateway.Models
{
    public class PayPalPaymentDetails
    {
        public string Id { get; set; }
        public string Intent { get; set; }
        public string State { get; set; }
        public string Cart { get; set; }
        public PayerViewModel Payer { get; set; }
        public List<TransactionViewModel> Transactions { get; set; }
        public RedirectUrlsViewModel RedirectUrls { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public List<LinkViewModel> Links { get; set; }
    }

    public class PayerViewModel
    {
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public PayerInfoViewModel PayerInfo { get; set; }
    }

    public class PayerInfoViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PayerId { get; set; }
        public ShippingAddressViewModel ShippingAddress { get; set; }
        public string CountryCode { get; set; }
    }

    public class ShippingAddressViewModel
    {
        public string RecipientName { get; set; }
        public string Line1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
    }

    public class TransactionViewModel
    {
        public AmountViewModel Amount { get; set; }
        public PayeeViewModel Payee { get; set; }
        public ItemListViewModel ItemList { get; set; }
        public List<object> RelatedResources { get; set; }
    }

    public class AmountViewModel
    {
        public string Total { get; set; }
        public string Currency { get; set; }
    }

    public class PayeeViewModel
    {
        public string MerchantId { get; set; }
        public string Email { get; set; }
    }

    public class ItemListViewModel
    {
        public ShippingAddressViewModel ShippingAddress { get; set; }
    }

    public class RedirectUrlsViewModel
    {
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
    }

    public class LinkViewModel
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
    }
}
