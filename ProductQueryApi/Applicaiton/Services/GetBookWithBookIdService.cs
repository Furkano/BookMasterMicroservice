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
    public class GetBookWithBookIdService : IRequestHandler<GetBookWithBookIdRequest,BaseResponse<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookWithBookIdService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }
        public async Task<BaseResponse<Book>> Handle(GetBookWithBookIdRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<Book> response = new BaseResponse<Book>();
            try
            {
                var book = await _bookRepository.SearchAsyncWithBookId<Book, int>(request.Id);
                if (book!=null)
                {
                    response.Data = book.FirstOrDefault(p => p.Id == request.Id);
                }
                else
                {
                    response.Errors.Add("Bu Id ile kitap bulunamadı.");
                    response.Data = null;
                }
                
            }
            catch (Exception e)
            {
                response.Errors.Add(e.Message);
                response.Data = null;
                Console.WriteLine(e);
            }

            return response;
        }
    }
}