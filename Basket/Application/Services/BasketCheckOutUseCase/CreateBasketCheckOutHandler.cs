using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Requests.BasketCheckOutRequest;
using BookMaster.Common.Events;
using Domain.Entities;
using Domain.IRepository;
using Mapster;
using MassTransit;
using MediatR;

namespace Application.Services.BasketCheckOutUseCase
{
    public class CreateBasketCheckOutHandler : IRequestHandler<CreateBasketCheckOutRequest,BaseResponseDto<Boolean>>
    {
        private readonly IBasketRepository<BasketChechOut> _repository;
        private readonly IBus _bus;

        public CreateBasketCheckOutHandler(
            IBasketRepository<BasketChechOut> repository,
            IBus bus
            )
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }
        public async Task<BaseResponseDto<bool>> Handle(CreateBasketCheckOutRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<Boolean> response = new BaseResponseDto<Boolean>();
            try
            {
                var basketCheckOut = new BasketChechOut
                {
                    Username = request.Username,
                    Address = request.Address,
                    CardName = request.CardName,
                    CardNumber = request.CardNumber,
                    CVC = request.CVC,
                    Email = request.Email,
                    Expiration = request.Expiration,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    TotalPrice = request.TotalPrice
                };
                var result = await _repository.Create(basketCheckOut);
                if (result!=null)
                {
                    response.Data = true;
                    await _bus.Publish(result.Adapt<CreateBasketCheckOutEvent>(),cancellationToken);
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