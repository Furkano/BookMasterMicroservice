using Domain.Entities;
using MediatR;

namespace Application.Dtos.Requests
{
    public class GetOrderWithIdRequest: IRequest<BaseResponseDto<Order>>
    {
        public int Id { get; set; }

        public GetOrderWithIdRequest(int id)
        {
            Id = id;
        }
    }
}