using System;
using System.Collections.Generic;
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
    public class GetOrderWithUsernameRequestHandler : IRequestHandler<GetOrderWithUsernameRequest,BaseResponseDto<List<Order>>>
    {
        private readonly IOrderRepository<Order> _repository;

        public GetOrderWithUsernameRequestHandler(IOrderRepository<Order> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<BaseResponseDto<List<Order>>> Handle(GetOrderWithUsernameRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<List<Order>> response = new BaseResponseDto<List<Order>>();
            try
            {
                var result = await _repository.Where(p => p.Username == request.Username)
                    .ToListAsync(cancellationToken: cancellationToken);
                if (result!=null)
                {
                    response.Data = result;
                    
                }
                else
                {
                    response.Errors.Add("No Order for this Username was found.");
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