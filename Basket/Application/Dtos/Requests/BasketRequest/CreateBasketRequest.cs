using Domain.Entities;
using MediatR;

namespace Application.Dtos.Requests.BasketRequest
{
    public class CreateBasketRequest : IRequest<BaseResponseDto<Basket>>
    {
        public string UserName { get; set; }
    }
}