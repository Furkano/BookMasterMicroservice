using System;
using System.Threading;
using System.Threading.Tasks;
using Applicaiton.Requests;
using Applicaiton.Responces;
using Core.ElasticOption.Option;
using Core.Entities;
using Applicaiton.Interaces;
using Core.IRepository;
using MediatR;

namespace Applicaiton.Services
{
    public class CreateBookService:IRequestHandler<CreateBookRequest,BaseResponse<Boolean>>
    {
        private readonly IBaseRepository _repository;
        private readonly ICategoryApiProxy _categoryApiProxy;
        public CreateBookService(IBaseRepository repository,
            ICategoryApiProxy categoryApiProxy)
        {
            _repository = repository;
            _categoryApiProxy = categoryApiProxy;
        }
        public async Task<BaseResponse<bool>> Handle(CreateBookRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<Boolean> response = new BaseResponse<bool>();
            try
            {
                Book book = new Book()
                {
                    Id = request.Id,
                    CategoryId = request.CategoryId,
                    Description = request.Description,
                    ImageUrl = request.ImageUrl,
                    Name = request.Name,
                    Price = request.Price
                };

                var resultCategoryApi = await _categoryApiProxy.GetCategoryById(book.CategoryId);
                if(resultCategoryApi !=null)
                {
                    book.CategoryName = resultCategoryApi.Name;
                    book.CategoryParent = resultCategoryApi.Parent;
                }

                book.Suggest = new Nest.CompletionField {Input = new SuggestItem().GetMember(book.CategoryName)};
                
                var result = await _repository.CreateAsync<Book, int>(book);
                response.Data = result;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new ElasticSearchException($"Insert Docuemnt failed at index {request.Name} :");
            }

            return response;
        }
    }
}