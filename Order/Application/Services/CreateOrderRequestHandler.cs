using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Requests;
using Domain.Entities;
using Domain.IRepository;
using MediatR;

namespace Application.Services
{
    public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest,BaseResponseDto<Order>>
    {
        private readonly IOrderRepository<Order> _repository;

        public CreateOrderRequestHandler(IOrderRepository<Order> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<BaseResponseDto<Order>> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<Order> response = new BaseResponseDto<Order>();
            try
            {
                Order order = new Order
                {
                    Address = request.Address,
                    CardName = request.CardName,
                    CardNumber = request.CardNumber,
                    CVC = request.CVC,
                    Email = request.Email,
                    Expiration = request.Expiration,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    TotalPrice = request.TotalPrice,
                    Username = request.Username
                };
                var result = await _repository.Create(order);
                if (result!=null)
                {
                    response.Data = result;
                }
                else
                {
                    response.Errors.Add("An error occurred while creating order in database.");
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