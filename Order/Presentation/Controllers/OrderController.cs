using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Requests;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpPost]
        public async Task<BaseResponseDto<Order>> CreateOrderAsync(CreateOrderRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete]
        public async Task<BaseResponseDto<Boolean>> DeleteOrderAsync(DeleteOrderRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("GetOrderWithUsername/{username}")]
        public async Task<BaseResponseDto<List<Order>>> GetOrderWithUsernameAsync(string username)
        {
            return await _mediator.Send(new GetOrderWithUsernameRequest(username));
        }
        [HttpGet("GetOrderWithId/{id}")]
        public async Task<BaseResponseDto<Order>> GetOrderWithIdAsync(int id)
        {
            return await _mediator.Send(new GetOrderWithIdRequest(id));
        }
    }
}