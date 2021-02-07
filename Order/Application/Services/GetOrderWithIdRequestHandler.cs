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
    public class GetOrderWithIdRequestHandler : IRequestHandler<GetOrderWithIdRequest,BaseResponseDto<Order>>
    {
        private readonly IOrderRepository<Order> _repository;
        private readonly IRedisRepository _redisRepository;

        public GetOrderWithIdRequestHandler(
            IOrderRepository<Order> repository,
            IRedisRepository redisRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _redisRepository=redisRepository ?? throw new ArgumentNullException(nameof(redisRepository));
        }
        public async Task<BaseResponseDto<Order>> Handle(GetOrderWithIdRequest request, CancellationToken cancellationToken)
        {
            BaseResponseDto<Order> responseDto = new BaseResponseDto<Order>();
            try
            {
                var redis = await _redisRepository.Get(request.Id.ToString());
                if (redis != null)
                {
                    responseDto.Data = redis;
                    return responseDto;
                }
                var mssql = await _repository.Where(p => p.Id == request.Id)
                         .FirstOrDefaultAsync(cancellationToken);
                if (mssql != null)
                {
                    responseDto.Data = mssql;
                    await _redisRepository.Set(mssql);
                }
                else
                {
                    responseDto.Errors.Add("No order for this Id was found");
                    responseDto.Data=null;
                }

                return responseDto;

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                responseDto.Errors.Add(exception.Message);
                return responseDto;
            }
            
        }
    }
}