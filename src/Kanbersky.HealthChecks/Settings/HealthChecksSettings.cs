namespace Kanbersky.HealthChecks.Settings
{
    /// <summary>
    /// Healtchecks Settings
    /// </summary>
    public class HealthChecksSettings
    {
        /// <summary>
        /// Send Webhook Notification is enable
        /// </summary>
        public bool EnableWebHook { get; set; }

        /// <summary>
        /// Notification Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Notification webhook url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Notification Payload
        /// </summary>
        public string Payload { get; set; } = "{\"text\": \"[[LIVENESS]] is failing with the error message : [[FAILURE]]\"}";

        /// <summary>
        /// Notification Restore Payload
        /// </summary>
        public string RestorePayload { get; set; } = "{\"text\": \"[[LIVENESS]] is recovered.All is up & running !\"}";
    }
}
