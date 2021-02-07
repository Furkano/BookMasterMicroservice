using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Applicaiton.Requests;
using Applicaiton.Responces;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nest.Specification.TasksApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IMediator _mediator;
        private readonly HttpClient _httpClient;
        public BookController(IMediator mediator,HttpClient httpClient)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        [HttpPost]
        public async Task<BaseResponse<Boolean>> CreateBookAsync(CreateBookRequest createBookRequest)
        {
            BaseResponse<Boolean> response = await _mediator.Send(createBookRequest);
            return response;
        }

        [HttpPut]
        public async Task<BaseResponse<Boolean>> UpdateBookAsync(UpdateBookRequest updateBookRequest)
        {
            BaseResponse<Boolean> response = await _mediator.Send(updateBookRequest);
            return response;
        }

        [HttpDelete]
        public async Task<BaseResponse<Boolean>> DeleteBookAsync(DeleteBookRequest deleteBookRequest)
        {
            BaseResponse<Boolean> response = await _mediator.Send(deleteBookRequest);
            return response;
        }

        [HttpGet("GetBookWithBookId")]
        public async Task<BaseResponse<Book>> GetBookWithBookIdAsync(GetBookWithBookIdRequest request)
        {
            BaseResponse<Book> response = await _mediator.Send(new GetBookWithBookIdRequest(request));
            return response;
        }
       
    }
}