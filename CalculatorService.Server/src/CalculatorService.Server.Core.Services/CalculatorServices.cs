using CalculatorService.Server.Core.Model.Entitites;
using CalculatorService.Server.Core.Model.Interfaces;

namespace CalculatorService.Server.Core.Services
{
  public class CalculatorServices : ICalculatorService
  {
    public Task<AddOperationResultDTO> AddElementsAsync(AddOperationDTO addOperationDTO)
    {
      return Task.FromResult(new AddOperationResultDTO()
      {
        Sum = addOperationDTO.Addends.Sum()
      });
    }
  }
}