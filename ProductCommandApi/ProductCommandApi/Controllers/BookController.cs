using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCommand.Requests;
using ApplicationCommand.ResponceModels;
using Microsoft.AspNetCore.Mvc;
using MediatR;
namespace ProductCommandApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        private static readonly string[] Summaries = new[]
        {
            "Kitab1", "Kitab2", "Kitab3", "Kitab4", "Kitab5", "Kitab6", "Kitab7", "Kitab8", "Kitab9", "Kitab10"
        };
        [HttpGet]
        public ActionResult Get()
        {
            var rng = new Random();
            return Ok(Summaries[rng.Next(Summaries.Length)]);
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
    }
}