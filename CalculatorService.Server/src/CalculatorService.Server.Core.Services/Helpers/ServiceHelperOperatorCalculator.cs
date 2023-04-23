using CalculatorService.Server.Core.Model.Entitites;

namespace CalculatorService.Server.Core.Services.Helpers
{
  internal class ServiceHelperOperatorCalculator
  {
    private delegate OperationResultDTO OperationToBePerfomed(OperationDTOOperands addOperationDTO);
    private readonly Dictionary<char, OperationToBePerfomed> _operationsToBePerfomed;

    internal ServiceHelperOperatorCalculator()
    {
      _operationsToBePerfomed = new Dictionary<char, OperationToBePerfomed>()
      {
        {ValidOperations.Sum,new OperationToBePerfomed(AddElements)}
      };
    }

    internal OperationResultDTO PerformOperation(OperationDTOOperands operands, char operation)
    {
      if (_operationsToBePerfomed.ContainsKey(operation))
      {
        return _operationsToBePerfomed[operation](operands);
      }
      else
      {
        throw new Exception($"Operation Not implemented for operator {operation}");
      }
    }

    private OperationResultDTO AddElements(OperationDTOOperands operands)
    {
      return new OperationResultDTO()
      {
        Result = operands.Operands.Sum()
      };
    }

  }
}
