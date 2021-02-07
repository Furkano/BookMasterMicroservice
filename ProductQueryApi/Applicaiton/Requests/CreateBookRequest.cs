using System;
using Applicaiton.Responces;
using MediatR;


namespace Applicaiton.Requests
{
    public class CreateBookRequest:IRequest<BaseResponse<Boolean>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
        
        public decimal Price { get; set; }
        
        public string CategoryId { get; set; }
    }
}