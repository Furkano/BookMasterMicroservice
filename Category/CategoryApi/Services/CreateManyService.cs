using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CategoryApi.Entities;
using CategoryApi.Repository.IRepository;
using CategoryApi.Requests;
using MediatR;
using MongoDB.Bson;

namespace CategoryApi.Services
{
    public class CreateManyService : IRequestHandler<CreateManyRequest,Boolean>
    {
        private readonly IRepository<Category> _repository;

        public CreateManyService(IRepository<Category> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<bool> Handle(CreateManyRequest requests, CancellationToken cancellationToken)
        {
            var response = false;
            try
            {
                List<Category> wiList = new List<Category>();
                foreach (var request in requests.Categories)
                {
                    var category = new Category()
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        Name = request.Name,
                        Parent = request.Parent
                    };
                    wiList.Add(category);
                }

                if (await _repository.CreateMany(wiList)) 
                {
                    response=true;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return response;
        }
    }
}