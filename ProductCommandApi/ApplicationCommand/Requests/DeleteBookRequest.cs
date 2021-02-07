using System;
using ApplicationCommand.ResponceModels;
using MediatR;

namespace ApplicationCommand.Requests
{
    public class DeleteBookRequest : IRequest<BaseResponse<Boolean>>
    {
        public int Id { get; set; }

        // public DeleteBookRequest(DeleteBookRequest model)
        // {
        //     Id = model.Id;
        // }
    }
}