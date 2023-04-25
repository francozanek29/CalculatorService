using CalculatorService.Server.Core.Model.Entitites;
using CalculatorService.Server.Core.Model.Interfaces;
using Serilog;

namespace CalculatorService.Server.Core.Services
{
  public class JournalService : IJournalService
  {
    private readonly IRepository _repository;
    private readonly ILogger _logger;

    public JournalService(IRepository repository, ILogger logger)
    {
      _repository = repository;
      _logger = logger;
    }

    public async Task<IEnumerable<OperationInfoDTO>> GetJornalOperationByIdAsync(string trackingId)
    {
      try
      {
        return await _repository.GetAllOperationInfosByTrackingIdAsync(trackingId);
      }
      catch (Exception ex)
      {
        _logger.Error(ex, "There was an error during the execution of the request to get the information from the Database");

        throw;
      }
    }
  }
}
