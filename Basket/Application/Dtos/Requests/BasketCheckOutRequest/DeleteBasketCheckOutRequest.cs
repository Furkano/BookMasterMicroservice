using System;
using MediatR;

namespace Application.Dtos.Requests.BasketCheckOutRequest
{
    public class DeleteBasketCheckOutRequest : IRequest<BaseResponseDto<Boolean>>
    {
        public int Id { get; set; }
    }
}