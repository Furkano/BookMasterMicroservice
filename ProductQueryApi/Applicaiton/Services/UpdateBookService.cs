using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Applicaiton.Requests;
using Applicaiton.Responces;
using Core.Entities;
using Core.IRepository;
using MediatR;
using Nest;

namespace Applicaiton.Services
{
    public class UpdateBookService:IRequestHandler<UpdateBookRequest,BaseResponse<Boolean>>
    {
        private readonly IBaseRepository _repository;

        public UpdateBookService(IBaseRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<BaseResponse<bool>> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<Boolean> response = new BaseResponse<bool>();
            try
            {
                var book = (await _repository.SearchAsyncWithBookId<Book, int>(request.Id))
                    .FirstOrDefault(p => p.Id == request.Id);
                if (book!=null)
                {
                    book.Name = request.Name;
                    book.ImageUrl = request.ImageUrl;
                    book.Description = request.Description;
                    book.Price = request.Price;
                    book.CategoryId = request.CategoryId;

                    response.Data=await _repository.UpdateAsync<Book, int>(book);
                }
                else
                {
                    response.Data = false;
                }
                
            }
            catch (Exception e)
            {
                response.Data = false;
                Console.WriteLine(e);
                throw;
            }

            return response;
        }
    }
}