using AutoMapper;
using CalculatorService.Server.Core.Model.Entitites;
using CalculatorService.Server.Core.Model.Interfaces;
using CalculatorService.Server.Repository.Entities;

namespace CalculatorService.Server.Repository
{
  public class JournalRepository : IRepository
  {
    private readonly JournalRepositoryDbContext _dbContext;
    private readonly IMapper _mapper;

    public JournalRepository(JournalRepositoryDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public async Task SaveOperationToRepositoryAsync(OperationDTO operationDTO)
    {
      _dbContext.Journeys.Add(_mapper.Map<OperationEntity>(operationDTO));

      await _dbContext.SaveChangesAsync();
    }
  }
}
