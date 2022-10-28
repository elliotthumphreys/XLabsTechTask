using Application.Exceptions;
using Application.Repository;
using AutoMapper;
using Domain;
using Dtos;

namespace Application.Tags
{
    internal class TagReader : IReadTag
    {
        private readonly IGenericRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;

        public TagReader(IGenericRepository<Tag> tagRepository,
                           IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public TagDto GetById(int tagId)
        {
            var tag = _tagRepository.GetById(tagId);

            if(tag is null)
                throw new NotFoundException();

            return _mapper.Map<TagDto>(tag);
        }

        public ICollection<TagDto> Get()
        {
            var tags = _tagRepository.Get().ToList();

            return _mapper.Map<ICollection<TagDto>>(tags);
        }
    }
}