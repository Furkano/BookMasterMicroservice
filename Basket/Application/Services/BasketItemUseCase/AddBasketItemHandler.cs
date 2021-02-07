using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Requests.BasketItemRequest;
using Domain.Entities;
using Domain.IRepository;
using MediatR;

namespace Application.Services.BasketItemUseCase
{
    public class AddBasketItemHandler : IRequestHandler<AddBasketItemRequest,BaseResponseDto<Boolean>>
    {
        private readonly IBasketRepository<BasketItem> _repository;

        public AddBasketItemHandler(IBasketRepository<BasketItem> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<BaseResponseDto<bool>> Handle(AddBasketItemRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<Boolean> response = new BaseResponseDto<Boolean>();
            try
            {
                var basketItem = new BasketItem
                {
                    BasketId = request.BasketId,
                    Price = request.Price,
                    ProductId = request.ProductId,
                    ProductName = request.ProductName,
                    Quantity = request.Quantity
                };
                var result = await _repository.Create(basketItem);
                if (result!=null)
                {
                    response.Data = true;
                }
                else
                {
                    response.Errors.Add("An error occurred while creating BasketItem data in database.");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                response.Data = false;
                response.Errors.Add(exception.Message);
            }

            return response;
        }
    }
}