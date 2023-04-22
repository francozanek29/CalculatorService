namespace CalculatorService.Server.Core.Model.Entitites
{
  /// <summary>
  /// Record in charge of handling the information for a Add operation parameters through the application. 
  /// It receives the data from the 
  /// Controller and send the data to the repository and viceversa. 
  /// </summary>
  public record AddOperationDTO
  {
    public IEnumerable<int> Addends { get; set; }
  }
}
