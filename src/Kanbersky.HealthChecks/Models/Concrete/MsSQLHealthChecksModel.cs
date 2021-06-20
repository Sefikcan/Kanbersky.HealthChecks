using Kanbersky.HealthChecks.Models.Abstract;

namespace Kanbersky.HealthChecks.Models.Concrete
{
    public class MsSQLHealthChecksModel : BaseHealthChecksModel
    {
        public string MsSQLConnectionString { get; set; }

        public string HealthQuery { get; set; }
    }
}
