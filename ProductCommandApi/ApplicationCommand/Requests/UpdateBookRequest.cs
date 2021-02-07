using System;
using ApplicationCommand.ResponceModels;
using MediatR;

namespace ApplicationCommand.Requests
{
    public class UpdateBookRequest : IRequest<BaseResponse<Boolean>>
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
        
        public decimal Price { get; set; }
        
        public string CategoryId { get; set; }

        // public UpdateBookRequest(UpdateBookRequest model)
        // {
        //     Id = model.Id;
        //     Name = model.Name;
        //     Description = model.Description;
        //     ImageUrl = model.ImageUrl;
        //     Price = model.Price;
        //     CategoryId = model.CategoryId;
        // }
    }
}