using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCommand.Requests;
using ApplicationCommand.ResponceModels;
using CoreCommand.IRepository;
using MediatR;

namespace ApplicationCommand.Services
{
    public class UpdateBookService : IRequestHandler<UpdateBookRequest,BaseResponse<Boolean>>
    {
        private readonly IBookRepository _repository;

        public UpdateBookService(IBookRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<BaseResponse<bool>> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<Boolean> response = new BaseResponse<bool>();
            try
            {
                var book = await _repository.GetBookById(request.Id);
                if (book == null)
                {
                    response.Errors.Add("Böyle bir kitap bulunamadı.");
                    response.Data = false;
                }
                else
                {
                    book.Name = request.Name;
                    book.ImageUrl = request.ImageUrl;
                    book.Description = request.Description;
                    book.Price = request.Price;
                    book.CategoryId = request.CategoryId;
                    await _repository.Update(book);
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