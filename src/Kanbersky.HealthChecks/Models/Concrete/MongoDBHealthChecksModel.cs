using Kanbersky.HealthChecks.Models.Abstract;

namespace Kanbersky.HealthChecks.Models.Concrete
{
    public class MongoDBHealthChecksModel : BaseHealthChecksModel
    {
        public string MongoDBConnectionString { get; set; }
    }
}
