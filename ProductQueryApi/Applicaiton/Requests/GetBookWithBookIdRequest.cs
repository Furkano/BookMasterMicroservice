using Applicaiton.Responces;
using Core.Entities;
using MediatR;

namespace Applicaiton.Requests
{
    public class GetBookWithBookIdRequest:IRequest<BaseResponse<Book>>
    {
        public int Id { get; set; }

        public GetBookWithBookIdRequest( GetBookWithBookIdRequest model)
        {
            Id = model.Id;
        }
    }
}