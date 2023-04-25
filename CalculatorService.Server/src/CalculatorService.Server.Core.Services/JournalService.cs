using CalculatorService.Server.Core.Model.Entitites;
using CalculatorService.Server.Core.Model.Interfaces;

namespace CalculatorService.Server.Core.Services
{
    public class JournalService : IJournalService
    {
        private readonly IRepository _repository;

        public JournalService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OperationInfoDTO>> GetJornalOperationByIdAsync(string trackingId)
        {
            return await _repository.GetAllOperationInfosByTrackingIdAsync(trackingId);
        }
    }
}
