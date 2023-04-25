namespace CalculatorService.Server.Core.Model.Entitites
{
  /// <summary>
  /// Record in charge of handling the information for a all operation parameters through the application. 
  /// It receives the data from the Controller and send the data to the repository and viceversa. 
  /// </summary>
  public record OperationDTOOperands
  {
    public IEnumerable<int> Operands { get; set; }
  }
}
