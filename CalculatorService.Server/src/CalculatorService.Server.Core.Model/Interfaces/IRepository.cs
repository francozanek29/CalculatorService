using CalculatorService.Server.Core.Model.Entitites;

namespace CalculatorService.Server.Core.Model.Interfaces
{
    /// <summary>
    /// Definition for all the methods that any Repository implementation should implement so the application 
    /// works as expected.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Method in charge of storage the operation in the database.
        /// </summary>
        /// <param name="operationDTO">Information to be storage</param>
        /// <returns></returns>
        Task SaveOperationToRepositoryAsync(OperationDTO operationDTO);

        /// <summary>
        /// Method in charge to get all the operation info objects given a tracking id
        /// </summary>
        /// <param name="trackingId">Tracking id for which we should get all the information</param>
        /// <returns>A List of OperationInfoDTO object in which we storage some values that are needed to be shown
        /// (see OperationInfoDTO definition)</returns>
        Task<IEnumerable<OperationInfoDTO>> GetAllOperationInfosByTrackingIdAsync(string trackingId);
    }
}
