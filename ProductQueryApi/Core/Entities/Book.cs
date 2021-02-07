using System;
using Core.ElasticOption.Option;

namespace Core.Entities
{
    public class Book:ElasticEntity<int>
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
        
        public decimal Price { get; set; }
        
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryParent { get; set; }
        
        
    }
}