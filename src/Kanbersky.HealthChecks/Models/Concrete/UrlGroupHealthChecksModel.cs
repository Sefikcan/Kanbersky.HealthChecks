using Kanbersky.HealthChecks.Models.Abstract;

namespace Kanbersky.HealthChecks.Models.Concrete
{
    public class UrlGroupHealthChecksModel : BaseHealthChecksModel
    {
        public string ApiUrl { get; set; }
    }
}
