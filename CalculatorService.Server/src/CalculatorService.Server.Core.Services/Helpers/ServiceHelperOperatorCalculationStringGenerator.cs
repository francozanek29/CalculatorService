using CalculatorService.Server.Core.Model.Entitites;

namespace CalculatorService.Server.Core.Services.Helpers
{
  internal class ServiceHelperOperatorCalculationStringGenerator
  {
    private delegate string StringGeneratorToBeUsed(OperationDTOOperands addOperationDTO, OperationResultDTO result, char operation);
    private readonly Dictionary<char, StringGeneratorToBeUsed> _stringGeneratorsToBeUsed;

    internal ServiceHelperOperatorCalculationStringGenerator()
    {
      _stringGeneratorsToBeUsed = new Dictionary<char, StringGeneratorToBeUsed>()
      {
        {ValidOperations.Sum, new StringGeneratorToBeUsed(SimpleStringGenerator)},
        {ValidOperations.Mult,new StringGeneratorToBeUsed(SimpleStringGenerator)},
        {ValidOperations.Diff,new StringGeneratorToBeUsed(SimpleStringGenerator)},
        {ValidOperations.Sqrt,new StringGeneratorToBeUsed(OneParameterFuntionStringGenerator)},
        {ValidOperations.Div, new StringGeneratorToBeUsed(DoubleResultStringGeneration)},
      };
    }

    internal string PerformOperation(OperationDTOOperands operands, char operation, OperationResultDTO result)
    {
      if (_stringGeneratorsToBeUsed.ContainsKey(operation))
      {
        return _stringGeneratorsToBeUsed[operation](operands, result,operation);
      }
      else
      {
        throw new Exception($"Operation Not implemented for operator {operation}");
      }
    }

    private string SimpleStringGenerator(OperationDTOOperands operators, OperationResultDTO result, char operation)
    {
      return $"{string.Join(operation, operators.Operands)}={result.Result}";
    }

    private string OneParameterFuntionStringGenerator(OperationDTOOperands operators, OperationResultDTO result, char operation)
    {
      return $"sqrt({operators.Operands.ElementAt(0)})={result.Result}";
    }

    private string DoubleResultStringGeneration(OperationDTOOperands operators, OperationResultDTO result, char operation) 
    { 
      var resultExtended = (OperationResultDTOExtension) result;

      return $"{string.Join(operation, operators.Operands)}={resultExtended.Result}-Remainder={resultExtended.ExtraResult}";
    }
  }
}
