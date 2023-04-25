namespace CalculatorService.Server.Core.Model.Entitites
{
    /// <summary>
    /// Entity to handle inside the application the journal query information for the journal controller
    /// </summary>
    public record OperationInfoDTO
    {
        public string Operation { get; set; }

        public string Calculation { get; set; }

        public string Date { get; set; }
    }
}
