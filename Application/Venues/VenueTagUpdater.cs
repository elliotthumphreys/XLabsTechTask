using Application.Exceptions;
using Application.Repository;
using AutoMapper;
using Domain;
using Dtos;
using Microsoft.EntityFrameworkCore;

namespace Application.Venues
{
    internal class VenueTagUpdater : IUpdateVenueTag
    {
        private readonly IGenericRepository<Venue> _venueRepository;
        private readonly IGenericRepository<Tag> _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VenueTagUpdater(IGenericRepository<Venue> venueRepository,
                              IGenericRepository<Tag> tagRepository,
                              IUnitOfWork unitOfWork,
                              IMapper mapper)
        {
            _venueRepository=venueRepository;
            _tagRepository=tagRepository;
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public ICollection<TagDto> Update(int venueId, ICollection<int> tagIds)
        {
            var venue = _venueRepository.Get(x => x.VenueId == venueId)
                                        .Include(x => x.VenueTags)
                                            .ThenInclude(x => x.Tag)
                                        .SingleOrDefault();

            if(venue is null)
                throw new NotFoundException();

            var tagIdsToRead = tagIds.Except(venue.Tags.Select(x => x.TagId));
            
            var allTags = _tagRepository.Get(x => tagIds.Contains(x.TagId)).ToList()
                                        .Union(venue.Tags);

            var tagIdsThatDontExist = tagIds.Except(allTags.Select(x => x.TagId));
            if(tagIdsThatDontExist.Any())
                throw new NotFoundException();

            venue.UpdateTags(tagIds, allTags);

            _unitOfWork.SaveChanges();

            return _mapper.Map<ICollection<TagDto>>(venue.Tags);
        }
    }
}
