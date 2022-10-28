using Application.Tags;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly IReadTag _tagReader;
        private readonly IDeleteTag _tagDeleter;
        private readonly ICreateTag _tagCreator;

        public TagsController(IReadTag tagReader,
                              IDeleteTag tagDeleter,
                              ICreateTag tagCreator)
        {
            _tagReader=tagReader;
            _tagDeleter=tagDeleter;
            _tagCreator=tagCreator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_tagReader.Get());
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateTagDto createTagDto)
        {
            return Ok(_tagCreator.Create(createTagDto));
        }

        [HttpGet("{tagId}")]
        public IActionResult Get([FromRoute]int tagId)
        {
            return Ok(_tagReader.GetById(tagId));
        }
        
        [HttpDelete("{tagId}")]
        public IActionResult Delete([FromRoute]int tagId)
        {
            _tagDeleter.DeleteById(tagId);

            return Ok();
        }
    }
}