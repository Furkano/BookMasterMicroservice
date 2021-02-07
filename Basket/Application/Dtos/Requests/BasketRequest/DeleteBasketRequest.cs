using System;
using MediatR;

namespace Application.Dtos.Requests.BasketRequest
{
    public class DeleteBasketRequest : IRequest<BaseResponseDto<Boolean>>
    {
        public int Id { get; set; }
    }
}