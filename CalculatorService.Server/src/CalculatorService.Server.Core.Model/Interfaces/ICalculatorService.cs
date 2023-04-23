using CalculatorService.Server.Core.Model.Entitites;

namespace CalculatorService.Server.Core.Model.Interfaces
{
  /// <summary>
  /// Definition for all the methods that should be used in the controller. We don´t care about the implementation at
  /// this point.
  /// </summary>
  public interface ICalculatorService
  {
    /// <summary>
    /// Add two or more operands and retrieve the result.
    /// </summary>
    /// <param name="addOperationDTO">An object which contains the elements to be added</param>
    /// <returns>An object with the result to be shown in the body</returns>
    Task<OperationResultDTO> AddElementsAsync(OperationDTOOperands operators, string trackingId);

    Task<OperationResultDTO> MultiplyElementsAsync(OperationDTOOperands operators, string trackingId);
  }
}
