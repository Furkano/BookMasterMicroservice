using System;
using System.Threading.Tasks;
using Applicaiton.Requests;
using BookMaster.Common.Events;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Applicaiton.Consumers
{
    public class BookCreatedConsumer : IConsumer<BookCreatedEvent>
    {
        private readonly ILogger<BookCreatedConsumer> _logger;
        private readonly IMediator _mediator;

        public BookCreatedConsumer(ILogger<BookCreatedConsumer> logger,IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task Consume(ConsumeContext<BookCreatedEvent> context)
        {
            var result = await _mediator.Send(context.Message.Adapt<CreateBookRequest>());
            _logger.LogInformation("Kitap yaratıldı.=>",result.Data);
        }
    }
}