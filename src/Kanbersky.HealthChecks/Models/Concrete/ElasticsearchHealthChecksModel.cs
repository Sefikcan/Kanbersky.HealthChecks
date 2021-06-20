using Kanbersky.HealthChecks.Models.Abstract;

namespace Kanbersky.HealthChecks.Models.Concrete
{
    public class ElasticsearchHealthChecksModel : BaseHealthChecksModel
    {
        public string ElasticsearchUrl { get; set; }
    }
}
