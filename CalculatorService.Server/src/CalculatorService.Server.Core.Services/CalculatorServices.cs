using CalculatorService.Server.Core.Model.Entitites;
using CalculatorService.Server.Core.Model.Interfaces;
using CalculatorService.Server.Core.Services.Helpers;

namespace CalculatorService.Server.Core.Services
{
    public class CalculatorServices : ICalculatorService
    {
        private readonly IRepository _journalRepository;
        private readonly RequestContext _requestContext;
        private readonly ServiceHelperOperatorCalculator _serviceHelperCalculatorOperator = new();
        private readonly ServiceHelperOperatorCalculationStringGenerator _serviceHelperOperatorCalculationStringGenerator = new();

        public CalculatorServices(IRepository repository, RequestContext requestContext)
        {
            _journalRepository = repository;
            _requestContext = requestContext;
        }

        public async Task<OperationResultDTO> ExecuteOperation(OperationDTOOperands operands, char operation)
        {
            try
            {
                var calculationResult = _serviceHelperCalculatorOperator.PerformOperation(operands, operation);

                if (!string.IsNullOrEmpty(_requestContext.TrackingId))
                {
                    var dto = GetOperationDTO(operation, calculationResult, operands);

                    await _journalRepository.SaveOperationToRepositoryAsync(dto);
                }

                return calculationResult;
            }
            catch
            {
                throw;
            }
        }

        private OperationDTO GetOperationDTO(char operation, OperationResultDTO result, OperationDTOOperands operators)
        {
            return new OperationDTO()
            {
                Date = DateTime.UtcNow.ToString("o"),
                TrackingId = _requestContext.TrackingId,
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