namespace CalculatorService.Server.Core.Model.Entitites
{
  /// <summary>
  /// Entity to handle inside the application the result for the Add operation
  /// </summary>
  public record AddOperationResultDTO
  {
    public int Sum { get; set; }
  }
}
