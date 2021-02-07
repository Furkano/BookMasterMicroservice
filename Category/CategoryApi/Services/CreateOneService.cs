using System;
using System.Threading;
using System.Threading.Tasks;
using CategoryApi.Entities;
using CategoryApi.Repository.IRepository;
using CategoryApi.Requests;
using MediatR;
using MongoDB.Bson;

namespace CategoryApi.Services
{
    public class CreateOneService : IRequestHandler<CreateOneRequest,Category>
    {
        private readonly IRepository<Category> _repository;

        public CreateOneService(IRepository<Category> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<Category> Handle(CreateOneRequest request, CancellationToken cancellationToken)
        {
            
            try
            {
                var category = new Category()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = request.Name,
                    Parent = request.Parent
                };
                if (await _repository.Create(category))
                {
                    return category;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return null;
        }
    }
}