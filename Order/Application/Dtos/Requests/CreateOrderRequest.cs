using System;
using Domain.Entities;
using MediatR;

namespace Application.Dtos.Requests
{
    public class CreateOrderRequest : IRequest<BaseResponseDto<Order>>
    {
        public string Username { get; set; }
        public decimal TotalPrice { get; set; }

        // BillingAddress
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Payment
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVC { get; set; }
    }
}