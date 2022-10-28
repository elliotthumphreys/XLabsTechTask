namespace Domain
{
    public class Venue
    {
        private HashSet<VenueTag> _venueTags { get; init; }
        private HashSet<Review> _reviews { get; init; }

        private Venue()
        {
            _venueTags = new HashSet<VenueTag>();
            _reviews = new HashSet<Review>();
        }

        public Venue(double latitude,
                     double longitude,
                     string name,
                     string venueUrl,
                     string thumbnailUrl,
                     string? phoneNumber,
                     string? twitterHandle,
                     string? address,
                     VenueType venueType)
            : this(latitude,
                   longitude,
                   name,
                   venueUrl,
                   thumbnailUrl,
                   phoneNumber,
                   twitterHandle,
                   address,
                   venueType,
                   Array.Empty<Tag>()){}

        public Venue(double latitude,
                   double longitude,
                   string name,
                   string venueUrl,
                   string thumbnailUrl,
                   string? phoneNumber,
                   string? twitterHandle,
                   string? address,
                   VenueType venueType,
                   ICollection<Tag> tags)
            : this()
        {
            Latitude = latitude;
            Longitude = longitude;

            Name = name;

            VenueType = venueType;

            VenueUrl = new Uri(venueUrl);
            ThumbnailUrl = new Uri(thumbnailUrl);

            Phone = !string.IsNullOrWhiteSpace(phoneNumber) ? phoneNumber : null;
            TwitterHandle = !string.IsNullOrWhiteSpace(twitterHandle) ? twitterHandle : null;
            Address = !string.IsNullOrWhiteSpace(address) ? address : null;

            _venueTags = tags.Select(x => new VenueTag(this, x)).ToHashSet();
        }

        public int VenueId { get; init; }

        public Uri VenueUrl { get; init; } = default!;
        public Uri ThumbnailUrl { get; init; } = default!;
 
        public string? Phone { get; init; } = default!;
        public string? TwitterHandle { get; init; } = default!;
        public string? Address { get; init; } = default!;

        public double Latitude { get; init; }
        public double Longitude { get; init; }

        public string Name { get; init; } = default!;

        public VenueType VenueType { get; set; } = default!;

        public IReadOnlyCollection<VenueTag> VenueTags => _venueTags;
        public IReadOnlyCollection<Review> Reviews => _reviews;
        
        public IReadOnlyCollection<Tag> Tags => _venueTags.Select(vt => vt.Tag).ToList();

        internal void AddReview(Review review)
        {
            _reviews.Add(review);
        }

        public void UpdateTags(ICollection<int> tagIds, IEnumerable<Tag> allTags)
        {
            _venueTags.RemoveWhere(vt => !tagIds.Contains(vt.TagId));
            
            var newTagIds = tagIds.Where(id => !Tags.Any(t => t.TagId == id));
            var newTags = allTags.IntersectBy(newTagIds, t => t.TagId);

            foreach(var tag in newTags)
                _venueTags.Add(new VenueTag(this, tag));
        }
    }
}
