using Kanbersky.HealthChecks.Models.Abstract;

namespace Kanbersky.HealthChecks.Models.Concrete
{
    public class PostgreSQLHealthChecksModel : BaseHealthChecksModel
    {
        public string PostgreSQLConnectionString { get; set; }

        public string HealthQuery { get; set; }
    }
}
