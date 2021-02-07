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
    public class GetBasketHandler : IRequestHandler<GetBasketRequest,BaseResponseDto<Basket>>
    {
        private readonly IBasketRepository<Basket> _repository;

        public GetBasketHandler(IBasketRepository<Basket> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<BaseResponseDto<Basket>> Handle(GetBasketRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<Basket> response = new BaseResponseDto<Basket>();
            try
            {
                var basket = await _repository.Where(p => p.UserName == request.UserName)
                    .Include(p => p.Items)
                    .FirstOrDefaultAsync(cancellationToken);
                if (basket!=null)
                {
                    response.Data = basket;
                }
                else
                {
                    response.Errors.Add("No Basket for this UserName was found.");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                response.Data = null;
                response.Errors.Add(exception.Message);
            }

            return response;
        }
    }
}