using Application.Exceptions;
using Application.Repository;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Tags
{
    internal class TagDeleter : IDeleteTag
    {
        private readonly IGenericRepository<Tag> _tagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TagDeleter(IGenericRepository<Tag> tagRepository,
                          IUnitOfWork unitOfWork)
        {
            _tagRepository = tagRepository;
            _unitOfWork=unitOfWork;
        }

        public void DeleteById(int tagId)
        {
            var tag = _tagRepository.Get(x => x.TagId == tagId)
                                            .Include(x => x.VenueTags)
                                        .SingleOrDefault();

            if(tag is null)
                throw new NotFoundException();

            _tagRepository.Delete(tag.TagId);

            _unitOfWork.SaveChanges();
        }
    }
}