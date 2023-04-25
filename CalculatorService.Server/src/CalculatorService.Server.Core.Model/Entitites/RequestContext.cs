namespace CalculatorService.Server.Core.Model.Entitites
{
    /// <summary>
    /// Entity to handle the Tracking Id value inside the application, the idea is to have one single point
    /// in which this value is handled.
    /// </summary>
    public record RequestContext
    {
        public string TrackingId { get; set; }
    }
}
