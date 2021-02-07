using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.Requests;
using Domain.Entities;
using Domain.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class DeleteOrderRequestHandler : IRequestHandler<DeleteOrderRequest,BaseResponseDto<Boolean>>
    {
        private readonly IOrderRepository<Order> _repository;

        public DeleteOrderRequestHandler(IOrderRepository<Order> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<BaseResponseDto<bool>> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<bool> response = new BaseResponseDto<bool>();
            try
            {
                var order = await _repository.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                if (order!=null)
                {
                    var result = await _repository.Delete(order);
                    response.Data = result;
                    if (!result)
                    {
                        response.Errors.Add("An error occurred while deleting data in database.");
                    }
                }
                else
                {
                    response.Errors.Add("No Order for this Id was found.");
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