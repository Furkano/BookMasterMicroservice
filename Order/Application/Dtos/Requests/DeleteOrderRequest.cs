using System;
using MediatR;

namespace Application.Dtos.Requests
{
    public class DeleteOrderRequest : IRequest<BaseResponseDto<Boolean>>
    {
        public int Id { get; set; }
    }
}