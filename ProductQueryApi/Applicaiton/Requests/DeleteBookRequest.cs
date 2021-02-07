using System;
using Applicaiton.Responces;
using MediatR;

namespace Applicaiton.Requests
{
    public class DeleteBookRequest:IRequest<BaseResponse<Boolean>>
    {
        public int Id { get; set; }
        
    }
}