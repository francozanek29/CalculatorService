using CalculatorService.Server.Core.Model.Entitites;

namespace CalculatorService.Server.Core.Model.Interfaces
{
    /// <summary>
    /// Definition for all the methods that should be used in the Journal controller. We don´t care about the implementation at
    /// this point.
    /// </summary>
    public interface IJournalService
    {
        /// <summary>
        /// Given a tracking Id this method will return all the operations that have that Id.
        /// </summary>
        /// <param name="trackingId"></param>
        /// <returns>List of operation for a specific tracking id</returns>
        Task<IEnumerable<OperationInfoDTO>> GetJornalOperationByIdAsync(string trackingId);
    }
}
