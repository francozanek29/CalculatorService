namespace CalculatorService.Server.Core.Model.Entitites
{
  public record OperationDTO
  {
    public string Operation { get; set; }

    public string Calculation { get; set; }

    public string TrackingId { get; set; }

    public string Date { get; set; }
  }
}
