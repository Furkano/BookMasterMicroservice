using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Requests.BasketItemRequest;
using Domain.Entities;
using Domain.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.BasketItemUseCase
{
    public class RemoveBasketItemHandler : IRequestHandler<RemoveBasketItemRequest,BaseResponseDto<Boolean>>
    {
        private readonly IBasketRepository<BasketItem> _repository;

        public RemoveBasketItemHandler(IBasketRepository<BasketItem> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<BaseResponseDto<bool>> Handle(RemoveBasketItemRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<Boolean> response = new BaseResponseDto<Boolean>();
            try
            {
                var basketItem = await _repository.Where(p => p.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                if (basketItem!=null)
                {
                    var result = await _repository.Delete(basketItem);
                    response.Data = result;
                    if (!result)
                    {
                        response.Errors.Add("An error occurred while deleting data in database.");
                    }
                }
                else
                {
                    response.Errors.Add("No BasketItem for this Id was found.");
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