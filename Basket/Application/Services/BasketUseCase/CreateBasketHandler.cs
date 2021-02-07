using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Requests.BasketRequest;
using Domain.Entities;
using Domain.IRepository;
using MediatR;

namespace Application.Services.BasketUseCase
{
    public class CreateBasketHandler : IRequestHandler<CreateBasketRequest,BaseResponseDto<Basket>>
    {
        private readonly IBasketRepository<Basket> _repository;

        public CreateBasketHandler(IBasketRepository<Basket> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<BaseResponseDto<Basket>> Handle(CreateBasketRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<Basket> response = new BaseResponseDto<Basket>();
            try
            {
                var basket = new Basket
                {
                    UserName = request.UserName
                };
                var result = await _repository.Create(basket);
                if (result!=null)
                {
                    response.Data = result;
                }
                else
                {
                    response.Errors.Add("An error occured while saving data to the database");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                response.Errors.Add(exception.Message);
            }

            return response;
        }
    }
}