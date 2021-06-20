using Kanbersky.HealthChecks.Models.Abstract;

namespace Kanbersky.HealthChecks.Models.Concrete
{
    public class MySQLHealthChecksModel : BaseHealthChecksModel
    {
        public string MySQLConnectionString { get; set; }
    }
}
