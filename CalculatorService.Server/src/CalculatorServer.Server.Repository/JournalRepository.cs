using AutoMapper;
using CalculatorService.Server.Core.Model.Entitites;
using CalculatorService.Server.Core.Model.Interfaces;
using CalculatorService.Server.Repository.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<OperationInfoDTO>> GetAllOperationInfosByTrackingIdAsync(string trackingId)
        {
            var operations = await  _dbContext.Journeys.Where(x => x.TrackingId == trackingId).ToListAsync();

            return _mapper.Map<IEnumerable<OperationInfoDTO>>(operations);
        }

        public async Task SaveOperationToRepositoryAsync(OperationDTO operationDTO)
        {
            _dbContext.Journeys.Add(_mapper.Map<OperationEntity>(operationDTO));

            await _dbContext.SaveChangesAsync();
        }
    }
}
