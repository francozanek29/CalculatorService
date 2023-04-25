namespace CalculatorService.Server.Core.Model.Entitites
{
    /// <summary>
    /// Entity to handle inside the application the operation info to be tracked and persisted in the DB
    /// </summary>
    public record OperationDTO
    {
        public string Operation { get; set; }

        public string Calculation { get; set; }

        public string TrackingId { get; set; }

        public string Date { get; set; }
    }
}
