using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCommand.Requests;
using ApplicationCommand.ResponceModels;
using BookMaster.Common.Events;
using CoreCommand.IRepository;
using Mapster;
using MassTransit;
using MediatR;

namespace ApplicationCommand.Services
{
    public class DeleteBookService : IRequestHandler<DeleteBookRequest,BaseResponse<Boolean>>
    {
        private readonly IBookRepository _repository;
        private readonly IBus _bus;

        public DeleteBookService(IBookRepository repository,IBus bus)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }
        public async Task<BaseResponse<bool>> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<Boolean> response = new BaseResponse<bool>();
            try
            {
                var book = await _repository.GetBookById(request.Id);
                if (book==null)
                {
                    response.Errors.Add("Böyle bir kitap bulunamadı.");
                    response.Data = false;
                }
                else
                {
                    await _repository.Delete(request.Id);
                    await _bus.Publish(request.Adapt<BookDeletedEvent>(),cancellationToken);
                    response.Data = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                response.Errors.Add(e.Message);
                response.Data = false;
            }

            return response;
        }
    }
}