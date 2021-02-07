using System.Collections.Generic;
using System.Linq;

namespace ApplicationCommand.ResponceModels
{
    public class BaseResponse<TData>
    {
        
        public bool HasError => Errors.Any();
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public TData Data { get; set; }
        
        public BaseResponse()
        {
            Errors = new List<string>();
        }
    }
}