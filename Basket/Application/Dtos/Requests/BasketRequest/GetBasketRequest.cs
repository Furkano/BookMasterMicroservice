using Domain.Entities;
using MediatR;

namespace Application.Dtos.Requests.BasketRequest
{
    public class GetBasketRequest : IRequest<BaseResponseDto<Basket>>
    {
        public string UserName { get; set; }
    }
}