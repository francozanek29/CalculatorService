using CalculatorService.Server.Core.Model.Entitites;

namespace CalculatorService.Server.Core.Model.Interfaces
{
    /// <summary>
    /// Definition for  the methods that should be used in the controller.
    /// </summary>
    public interface ICalculatorService
    {
        /// <summary>
        /// Execute the math operation given a list of Operands and the operation.
        /// </summary>
        /// <param name="operands"></param>
        /// <param name="operation"></param>
        /// <returns>OperationResultDTO in which the result or results of the operation is/are</returns>
        Task<OperationResultDTO> ExecuteOperation(OperationDTOOperands operands, char operation);       
    }
}
