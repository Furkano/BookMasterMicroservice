using System;
using System.Collections.Generic;
using CategoryApi.Entities;
using MediatR;

namespace CategoryApi.Requests
{
    public class CreateManyRequest : IRequest<Boolean>
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}