using System;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Requests.BasketCheckOutRequest;
using Application.Dtos.Requests.BasketItemRequest;
using Application.Dtos.Requests.BasketRequest;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private readonly IMediator _mediator;
        public BasketController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("CreateBasket")]
        public async Task<BaseResponseDto<Basket>> CreateBasketAsync(CreateBasketRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete("DeleteBasket")]
        public async Task<BaseResponseDto<Boolean>> DeleteBasketAsync(DeleteBasketRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("GetBasket")]
        public async Task<BaseResponseDto<Basket>> GetBasketAsync(GetBasketRequest request)
        {
            return await _mediator.Send(request);
        }
        [HttpPost("AddBasketItem")]
        public async Task<BaseResponseDto<Boolean>> AddBasketItemAsync(AddBasketItemRequest request)
        {
            return await _mediator.Send(request);
        }
        [HttpDelete("RemoveBasketItem")]
        public async Task<BaseResponseDto<Boolean>> RemoveBasketItemAsync(RemoveBasketItemRequest request)
        {
            return await _mediator.Send(request);
        }
        [HttpPost("CreateBasketCheckOut")]
        public async Task<BaseResponseDto<Boolean>> CreateBasketCheckOutAsync(CreateBasketCheckOutRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}