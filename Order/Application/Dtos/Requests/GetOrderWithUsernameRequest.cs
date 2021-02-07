using System.Collections.Generic;
using Domain.Entities;
using MediatR;

namespace Application.Dtos.Requests
{
    public class GetOrderWithUsernameRequest : IRequest<BaseResponseDto<List<Order>>>
    {
        public string Username { get; set; }

        public GetOrderWithUsernameRequest(string username)
        {
            Username = username;
        }
    }
}