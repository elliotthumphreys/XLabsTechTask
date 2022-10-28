using Application.Reviews;
using Application.Venues;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VenuesController : ControllerBase
    {
        private readonly IReadVenue _venueReader;
        private readonly IReadReview _reviewReader;
        private readonly IDeleteReview _reviewDeleter;
        private readonly IDeleteVenue _venueDeleter;
        private readonly ICreateVenue _venueCreator;
        private readonly ICreateReview _reviewCreator;
        private readonly IReadVenueTag _venueTagReader;
        private readonly IUpdateVenueTag _venueTagUpdater;

        public VenuesController(IReadVenue venueReader,
                                IReadReview reviewReader,
                                IDeleteReview reviewDeleter,
                                IDeleteVenue venueDeleter,
                                ICreateVenue venueCreator,
                                ICreateReview reviewCreator,
                                IReadVenueTag venueTagReader,
                                IUpdateVenueTag venueTagUpdater)
        {
            _venueReader = venueReader;
            _reviewReader = reviewReader;
            _reviewDeleter = reviewDeleter;
            _venueDeleter = venueDeleter;
            _venueCreator = venueCreator;
            _reviewCreator = reviewCreator;
            _venueTagReader=venueTagReader;
            _venueTagUpdater=venueTagUpdater;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_venueReader.Get());
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateVenueDto createVenueDto)
        {
            return Ok(_venueCreator.Create(createVenueDto));
        }

        [HttpGet("{venueId}")]
        public IActionResult Get([FromRoute]int venueId)
        {
            return Ok(_venueReader.GetById(venueId));
        }
        
        [HttpDelete("{venueId}")]
        public IActionResult Delete([FromRoute]int venueId)
        {
            _venueDeleter.DeleteById(venueId);

            return Ok();
        }
        
        [HttpGet("{venueId}/Tags")]
        public IActionResult GetTags([FromRoute]int venueId)
        {
            return Ok(_venueTagReader.GetById(venueId));
        }
        
        [HttpPut("{venueId}/Tags")]
        public IActionResult UpdateTags([FromRoute]int venueId,
                                        [FromBody]ICollection<int> tagIds)
        {
            return Ok(_venueTagUpdater.Update(venueId, tagIds));
        }

        [HttpGet("{venueId}/Reviews")]
        public IActionResult GetReviewsForVenue([FromRoute]int venueId)
        {
            return Ok(_reviewReader.Get(venueId));
        }
        
        [HttpPost("{venueId}/Reviews")]
        public IActionResult CreateReview([FromRoute]int venueId,
                                          [FromBody]CreateReviewDto createReviewDto)
        {
            return Ok(_reviewCreator.Create(venueId, createReviewDto));
        }

        [HttpGet("{venueId}/Reviews/{reviewId}")]
        public IActionResult GetReviewForVenue([FromRoute]int venueId,
                                               [FromRoute]int reviewId)
        {
            return Ok(_reviewReader.GetById(venueId, reviewId));
        }
        
        [HttpDelete("{venueId}/Reviews/{reviewId}")]
        public IActionResult DeleteReview([FromRoute]int venueId,
                                          [FromRoute]int reviewId)
        {
            _reviewDeleter.DeleteById(venueId, reviewId);

            return Ok();
        }
    }
}