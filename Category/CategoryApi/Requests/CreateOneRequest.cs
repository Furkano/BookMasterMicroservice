#nullable enable
using System;
using CategoryApi.Entities;
using MediatR;

namespace CategoryApi.Requests
{
    public class CreateOneRequest : IRequest<Category>
    {
        public string Name { get; set; } = null!;
        public string? Parent { get; set; }
    }
}