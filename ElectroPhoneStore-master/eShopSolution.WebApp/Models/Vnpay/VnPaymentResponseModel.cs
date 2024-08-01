using eShopSolution.ViewModels.Sales;
using eShopSolution.ViewModels.Utilities.Enums;
using System;
using System.Collections.Generic;

namespace eShopSolution.WebApp.Models.Vnpay
{

    public class VnPaymentResponseModel
    {
        public double Amount { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; } // Thêm thuộc tính PhoneNumber
        public bool Success { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderDescription { get; set; }
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
    public class VnPaymentRequestModel
    {
        public string Adress { get; set; } // Đổi kiểu từ double thành string
        public string Phonenumber { get; set; } // Đổi kiểu từ double thành string
        public double ProductId { get; set; }
        public int OrderId { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public double PhoneNumber { get; set; } // Kiểu này cần được sửa lại thành string
        public DateTime CreatedDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}