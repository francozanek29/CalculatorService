using CalculatorService.Server.Core.Model.Entitites;

namespace CalculatorService.Server.Core.Model.Interfaces
{
  public interface IRepository
  {
    Task SaveOperationToRepositoryAsync(OperationDTO operationDTO);

    Task<IEnumerable<OperationInfoDTO>> GetAllOperationInfosByTrackingIdAsync(string trackingId); 
  }
}
