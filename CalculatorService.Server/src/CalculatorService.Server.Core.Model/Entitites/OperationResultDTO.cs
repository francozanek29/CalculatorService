namespace CalculatorService.Server.Core.Model.Entitites
{
  /// <summary>
  /// Entity to handle inside the application the result for the all the operations, the idea is to generelized
  /// everything in a single class so we don´t have to many classes and the code is clean and mantainable.
  /// </summary>
  public record OperationResultDTO
  {
    public int Result { get; set; }
  }
}
