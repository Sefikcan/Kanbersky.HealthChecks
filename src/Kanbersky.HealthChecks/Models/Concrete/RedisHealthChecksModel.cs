using Kanbersky.HealthChecks.Models.Abstract;

namespace Kanbersky.HealthChecks.Models.Concrete
{
    public class RedisHealthChecksModel : BaseHealthChecksModel
    {
        public string RedisConnectionString { get; set; }
    }
}
