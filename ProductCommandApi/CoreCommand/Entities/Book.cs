using System;

namespace CoreCommand.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
        
        public decimal Price { get; set; }
        
        public string CategoryId { get; set; }
   
    }
}
