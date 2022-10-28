namespace Domain
{
    public class Tag
    {
        private HashSet<VenueTag> _venueTags;

        private Tag()
        {
            _venueTags = new HashSet<VenueTag>();
        }

        public Tag(string name) : this()
        {
            Name = name.Trim();
        }

        public int TagId { get; init; }
        public string Name { get; init; } = default!;

        public IReadOnlyCollection<VenueTag> VenueTags => _venueTags;
    }
}