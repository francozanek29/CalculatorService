using CalculatorService.Server.Core.Model.Entitites;
using CalculatorService.Server.Core.Model.Interfaces;
using CalculatorService.Server.Core.Services.Helpers;

namespace CalculatorService.Server.Core.Services
{
  public class CalculatorServices : ICalculatorService
  {
    private readonly IRepository _journalRepository;
    private readonly ServiceHelperOperatorCalculator _serviceHelperCalculatorOperator = new();
    private readonly ServiceHelperOperatorCalculationStringGenerator _serviceHelperOperatorCalculationStringGenerator = new();

    public CalculatorServices(IRepository repository)
    {
      _journalRepository = repository;
    }

    public async Task<OperationResultDTO> AddElementsAsync(OperationDTOOperands operators, string trackingId)
    {
      return await ExecuteOperation(operators, trackingId, ValidOperations.Sum);
    }

    public async Task<OperationResultDTO> MultiplyElementsAsync(OperationDTOOperands factors, string trackingId)
    {
      return await ExecuteOperation(factors, trackingId, ValidOperations.Mult);
    }

    public async Task<OperationResultDTO> SubElementsAsync(OperationDTOOperands subsOperators, string trackingId)
    {
      return await ExecuteOperation(subsOperators, trackingId, ValidOperations.Diff);
    }

    public async Task<OperationResultDTO> SqrtElementsAsync(OperationDTOOperands sqrtNumber, string trackingId)
    {
      return await ExecuteOperation(sqrtNumber, trackingId, ValidOperations.Sqrt);
    }

    public async Task<OperationResultDTO> DivElementsAsync(OperationDTOOperands operators, string trackingId)
    {
      return await ExecuteOperation(operators, trackingId, ValidOperations.Div);
    }

    private async Task<OperationResultDTO> ExecuteOperation(OperationDTOOperands operands, string trackingId, char operation)
    {
      try
      {
        var calculationResult = _serviceHelperCalculatorOperator.PerformOperation(operands, operation);

        if (!string.IsNullOrEmpty(trackingId))
        {
          var dto = GetOperationDTO(operation, trackingId, calculationResult, operands);

          await _journalRepository.SaveOperationToRepositoryAsync(dto);
        }

        return calculationResult;
      }
      catch
      {
        throw;
      }
    }

    private OperationDTO GetOperationDTO(char operation, string trackingId, OperationResultDTO result, OperationDTOOperands operators)
    {
      return new OperationDTO()
      {
        Date = DateTime.UtcNow.ToString("o"),
        TrackingId = trackingId,
        Operation = GetOperatioName(operation),
        Calculation = _serviceHelperOperatorCalculationStringGenerator.PerformOperation(operators, operation, result)
      };
    }

    private string GetOperatioName(char operationOperator)
    {
      switch (operationOperator)
      {
        case (ValidOperations.Sum):
          return "Sum";
        case (ValidOperations.Mult):
          return "Mul";
        case (ValidOperations.Diff):
          return "Dif";
        case (ValidOperations.Sqrt):
          return "Sqr";
        case (ValidOperations.Div):
          return "Div";
        default: return string.Empty;
      }
    }
  }
}