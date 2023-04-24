namespace CalculatorService.Server.Core.Model.Entitites
{
  public record OperationResultDTOExtension : OperationResultDTO
  {
    public int ExtraResult { get; set; }
  }
}
