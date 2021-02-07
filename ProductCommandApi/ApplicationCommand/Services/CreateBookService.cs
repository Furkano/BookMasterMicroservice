using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCommand.Requests;
using ApplicationCommand.ResponceModels;
using BookMaster.Common.Events;
using CoreCommand.Entities;
using CoreCommand.IRepository;
using Mapster;
using MassTransit;
using MediatR;

namespace ApplicationCommand.Services
{
    public class CreateBookService : IRequestHandler<CreateBookRequest,BaseResponse<Boolean>>
    {
        private readonly IBookRepository _repository;
        private readonly IBus _bus;
        public CreateBookService(IBookRepository repository,IBus bus)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }
        public async Task<BaseResponse<bool>> Handle(CreateBookRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<Boolean> response = new BaseResponse<bool>();
            try
            {
                Book book = new Book()
                {
                    CategoryId = request.CategoryId,
                    Description = request.Description,
                    ImageUrl = request.ImageUrl,
                    Name = request.Name,
                    Price = request.Price
                };
                var book2 = (await _repository.Create(book));
                await _bus.Publish(book2.Adapt<BookCreatedEvent>(), cancellationToken);
                response.Data = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.Errors.Add(e.Message+"*********InnerException**********"+e.InnerException);
            }

            return response;
        }
    }
}