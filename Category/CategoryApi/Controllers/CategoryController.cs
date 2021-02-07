using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CategoryApi.Entities;
using CategoryApi.Repository.IRepository;
using CategoryApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CategoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _repository;
        private readonly IMediator _mediator;

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            IRepository<Category> repository,
            ILogger<CategoryController> logger,
            IMediator mediator
            )
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _repository.GetCategories();
            return Ok(categories);
        }
        [Route("[action]/{parent}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryByParent(string parent)
        {
            var parents = await _repository.GetCategoryByParent(parent);
            return Ok(parents);
        }
        
        [HttpGet("{id:length(24)}", Name = "GetCategoryById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Category>> GetCategoryById(string id)
        {
            var category = await _repository.GetCategoryById(id);
        
            if (category == null)
            {
                _logger.LogError($"Category with id: {id}, hasn't been found in database.");
                return NotFound();
            }
        
            return Ok(category);
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Category>> Create([FromBody] CreateOneRequest createOne)
        {
            var result = await _mediator.Send(createOne);
            if (result!=null)
            {
                return CreatedAtRoute("GetCategoryById", new { id = result.Id }, result);
            }
            else
            {
                _logger.LogError($"Category with id: {createOne.Name}, hasn't been created.");
                return StatusCode((int)HttpStatusCode.BadGateway);
            }
        }
        [HttpPost("CreateMany")]
        [ProducesResponseType(typeof(void),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateMany([FromBody] CreateManyRequest createManyRequest)
        {
            var result = await _mediator.Send(createManyRequest);
            return Ok(result);
        }
        [HttpPut]
        [ProducesResponseType(typeof(void),  (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Category value)
        {
            return Ok(await _repository.Update(value));
        }
        
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _repository.Delete(id));
        }
    }
}