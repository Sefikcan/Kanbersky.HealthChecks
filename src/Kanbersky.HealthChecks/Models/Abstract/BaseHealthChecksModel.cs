using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Kanbersky.HealthChecks.Models.Abstract
{
    public abstract class BaseHealthChecksModel
    {
        public string Name { get; set; }

        public HealthStatus? FailureStatus { get; set; }
    }
}
