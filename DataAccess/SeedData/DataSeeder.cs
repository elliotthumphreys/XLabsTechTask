using CsvHelper;
using Domain;
using System.Globalization;
using System.Reflection;

namespace DataAccess.SeedData
{
    public static class DataSeeder
    {
        public static void Seed(this BeerQuestDbContext dbContext)
        {
            if(dbContext.Set<Venue>().Any())
                return;

            var pathToSeedData = Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location)!.FullName, "SeedData/leedsbeerquest.csv");

            using var reader = new StreamReader(pathToSeedData);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                
            var records = csv.GetRecords<dynamic>().ToList();

            var tagRecords = records.SelectMany(r => ((string)r.tags).Split(','))
                                   .Distinct()
                                   .Where(t => !string.IsNullOrWhiteSpace(t))
                                   .OrderBy(t => t)
                                   .Select((t, index) => new Tag(t))
                                   .ToList();

            var venueRecords = records.Select((r, index) =>
            {
                var tags = tagRecords.IntersectBy(((string)r.tags).Split(','), x => x.Name);

                var venueType = r.category switch
                {
                    "Pub reviews" => VenueType.Pub,
                    "Uncategorized" => VenueType.Unclassified,
                    "Closed venues" => VenueType.Closed,
                    "Bar reviews" => VenueType.Bar,
                    "Other reviews" => VenueType.Other,
                    _ => throw new Exception($"Venue type: {r.category} not configured")
                };

                var venue = new Venue(Convert.ToDouble(r.lat),
                                  Convert.ToDouble(r.lng),
                                  (string)r.name,
                                  r.url,
                                  r.thumbnail,
                                  r.phone,
                                  r.twitter,
                                  r.address,
                                  venueType,
                                  tags.ToList());

                _ = new Review(Convert.ToDecimal(r.stars_beer),
                               Convert.ToDecimal(r.stars_atmosphere),
                               Convert.ToDecimal(r.stars_amenities),
                               Convert.ToDecimal(r.stars_value),
                               (string)r.excerpt,
                               Convert.ToDateTime(r.date),
                               venue);

                return venue;
            }).ToList();

            dbContext.AddRange(venueRecords);

            dbContext.SaveChanges();
        }
    }
}
