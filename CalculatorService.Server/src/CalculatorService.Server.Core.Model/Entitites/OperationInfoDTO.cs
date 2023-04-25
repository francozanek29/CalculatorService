namespace CalculatorService.Server.Core.Model.Entitites
{
    public record OperationInfoDTO
    {
        public string Operation { get; set; }

        public string Calculation { get; set; }

        public string Date { get; set; }
    }
}
