using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Applicaiton.Requests;
using Applicaiton.Responces;
using Core.Entities;
using Core.IRepository;
using MediatR;

namespace Applicaiton.Services
{
    public class DeleteBookService:IRequestHandler<DeleteBookRequest,BaseResponse<Boolean>>
    {
        private readonly IBookRepository _repository;
        public DeleteBookService(IBookRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<BaseResponse<bool>> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<Boolean> response = new BaseResponse<bool>();
            try
            {
                var deleteObject = (await _repository.SearchAsyncWithBookId<Book, int>(request.Id)).FirstOrDefault(p=>p.Id==request.Id);
                if (deleteObject != null)
                {
                    var deleteBook = await _repository.DeleteAsync<Book, int>(deleteObject);
                    if (deleteBook)
                    {
                        response.Data = true;
                    }
                    else
                    {
                        response.Errors.Add("Kitap silinirken hata oluştu.");
                        response.Data = true;
                    }
                }
                else
                {
                    response.Errors.Add("Silinecek kitap null geldi.");
                    response.Data = false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                response.Errors.Add(exception.Message);
                response.Data = false;
            }

            return response;
        }
        
    }
}