using CalculatorService.Server.Core.Model.Entitites;
using CalculatorService.Server.Core.Model.Interfaces;
using CalculatorService.Server.Core.Services.Helpers;

namespace CalculatorService.Server.Core.Services
{
  public class CalculatorServices : ICalculatorService
  {
    private readonly IRepository _journalRepository;
    private readonly ServiceHelperOperatorCalculator _serviceHelperCalculatorOperator = new();

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

    private async Task<OperationResultDTO> ExecuteOperation(OperationDTOOperands operands, string trackingId, char operation)
    {
      try
      {
        var calculationResult = _serviceHelperCalculatorOperator.PerformOperation(operands, operation);

        if (!string.IsNullOrEmpty(trackingId))
        {
          var dto = GetOperationDTO(operation, trackingId, calculationResult.Result, operands.Operands);

          await _journalRepository.SaveOperationToRepositoryAsync(dto);
        }

        return calculationResult;
      }
      catch
      {
        throw;
      }
    }

    private OperationDTO GetOperationDTO(char operation, string trackingId, int result, IEnumerable<int> operators)
    {
      return new OperationDTO()
      {
        Date = DateTime.UtcNow.ToString("o"),
        TrackingId = trackingId,
        Operation = GetOperatioName(operation),
        Calculation = string.Join(operation, operators) + "=" + result
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
        default: return string.Empty;
      }
    }
  }
}