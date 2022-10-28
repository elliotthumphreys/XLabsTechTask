using Application.Exceptions;
using Application.Repository;
using AutoMapper;
using Domain;
using Dtos;

namespace Application.Tags
{
    internal class TagCreator : ICreateTag
    {
        private readonly IGenericRepository<Tag> _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagCreator(IGenericRepository<Tag> tagRepository,
                          IMapper mapper,
                          IUnitOfWork unitOfWork)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public TagDto Create(CreateTagDto createTagDto)
        {
            if(string.IsNullOrWhiteSpace(createTagDto.Name))
                throw new ValidationException($"Value for property {nameof(CreateTagDto.Name)} must be provided");

            if(_tagRepository.Get(x => x.Name == createTagDto.Name.Trim()).Any())
                throw new ValidationException($"Tag with {nameof(CreateTagDto.Name)} '{createTagDto.Name}' already exists");

            var venue = new Tag(createTagDto.Name);

            _tagRepository.Insert(venue);

            _unitOfWork.SaveChanges();

            return _mapper.Map<TagDto>(venue);
        }
    }
}