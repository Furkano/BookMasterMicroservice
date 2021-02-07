using System;
using MediatR;

namespace Application.Dtos.Requests.BasketItemRequest
{
    public class RemoveBasketItemRequest : IRequest<BaseResponseDto<Boolean>>
    {
        public int Id { get; set; }
    }
}