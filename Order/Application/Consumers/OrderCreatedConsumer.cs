using System;
using System.Threading.Tasks;
using Application.Dtos.Requests;
using BookMaster.Common.Events;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Consumers
{
    public class OrderCreatedConsumer : IConsumer<CreateBasketCheckOutEvent>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;
        private readonly IMediator _mediator;

        public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger,IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task Consume(ConsumeContext<CreateBasketCheckOutEvent> context)
        {
            var result = await _mediator.Send(context.Message.Adapt<CreateOrderRequest>());
            _logger.LogInformation("Sipariş yaratıldı.=>",result.Data);
        }
    }
}