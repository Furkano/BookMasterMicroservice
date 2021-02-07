using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Requests.BasketRequest;
using Domain.Entities;
using Domain.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.BasketUseCase
{
    public class DeleteBasketHandler : IRequestHandler<DeleteBasketRequest,BaseResponseDto<Boolean>>
    {
        private readonly IBasketRepository<Basket> _repository;

        public DeleteBasketHandler(IBasketRepository<Basket> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<BaseResponseDto<bool>> Handle(DeleteBasketRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<Boolean> response = new BaseResponseDto<Boolean>();
            try
            {
                var basket = await _repository.Where(p => p.Id == request.Id)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                if (basket!=null)
                {
                    var result = await _repository.Delete(basket);
                    response.Data = result;
                    if (!result)
                    {
                        response.Errors.Add("An error occurred while deleting data in database.");
                    }
                }
                else
                {
                    response.Errors.Add("No Basket for this Id was found.");
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