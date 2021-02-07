using System;
using System.Threading.Tasks;
using Applicaiton.Requests;
using BookMaster.Common.Events;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Applicaiton.Consumers
{
    public class BookDeletedConsumer: IConsumer<BookDeletedEvent>
    {
        private readonly ILogger<BookDeletedConsumer> _logger;
        private readonly IMediator _mediator;

        public BookDeletedConsumer(ILogger<BookDeletedConsumer> logger,IMediator mediator)
        {
            
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task Consume(ConsumeContext<BookDeletedEvent> context)
        {
            var result = await _mediator.Send(context.Adapt<DeleteBookRequest>());
            _logger.LogInformation("Kitab silindi",result);
        } 
    }
}