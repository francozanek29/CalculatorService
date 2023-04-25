using CalculatorService.Server.Core.Model.Entitites;

namespace CalculatorService.Server.Core.Model.Interfaces
{
    public interface IJournalService
    {
        Task<IEnumerable<OperationInfoDTO>> GetJornalOperationByIdAsync(string trackingId);
    }
}
