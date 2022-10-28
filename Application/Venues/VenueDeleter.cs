using Application.Exceptions;
using Application.Repository;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Venues
{
    internal class VenueDeleter : IDeleteVenue
    {
        private readonly IGenericRepository<Venue> _venueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VenueDeleter(IGenericRepository<Venue> venueRepository,
                            IUnitOfWork unitOfWork)
        {
            _venueRepository = venueRepository;
            _unitOfWork=unitOfWork;
        }

        public void DeleteById(int venueId)
        {
            var venue = _venueRepository.Get(x => x.VenueId == venueId)
                                            .Include(x => x.Reviews)
                                        .SingleOrDefault();

            if(venue is null)
                throw new NotFoundException();

            _venueRepository.Delete(venue.VenueId);

            _unitOfWork.SaveChanges();
        }
    }
}