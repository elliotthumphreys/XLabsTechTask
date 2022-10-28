namespace Domain
{
    public class VenueTag
    {
        private VenueTag()
        {

        }

        public VenueTag(Venue venue, Tag tag) : this()
        {
            Venue = venue;
            VenueId = venue.VenueId;

            Tag = tag;
            TagId = tag.TagId;
        }

        public int VenueId { get; init; }
        public int TagId { get; init; }

        public Venue Venue { get; init; } = default!;
        public Tag Tag { get; init; } = default!;
    }
}
