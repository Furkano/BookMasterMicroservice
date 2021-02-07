using System;
using MediatR;

namespace Application.Dtos.Requests.BasketItemRequest
{
    public class AddBasketItemRequest : IRequest<BaseResponseDto<Boolean>>
    {
        public int Quantity { get; set; }
        public int BasketId { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
}