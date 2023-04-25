using AutoMapper;
using CalculatorService.Server.Core.Model.Entitites;
using CalculatorService.Server.Core.Model.Interfaces;
using CalculatorService.Server.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.Server.WebAPI.Controllers
{
    /// <summary>
    /// Controller to handle all the operation to be performed in the application.
    /// </summary>
    [Route("calculator")]
    public class CalculatorController : Controller
    {
        private readonly ICalculatorService _calculatorService;
        private readonly IMapper _mapper;

        public CalculatorController(ICalculatorService calculatorService, IMapper mapper)
        {
            _calculatorService = calculatorService;
            _mapper = mapper;
        }

        /// <summary>
        /// Add two or more operands and retrieve the result.
        /// </summary>
        /// <param name="addends"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult<AddOperationResultModel>> AddElementsAsync([FromBody] AddOperationModel addends)
        {
            var addOperationResult = await _calculatorService.ExecuteOperation(_mapper.Map<OperationDTOOperands>(addends),ValidOperations.Sum);

            return Ok(_mapper.Map<AddOperationResultModel>(addOperationResult));
        }

        /// <summary>
        /// Multiply two or more operands and retrieve the result.
        /// </summary>
        /// <param name="factors"></param>
        /// <returns></returns>
        [HttpPost("mult")]
        public async Task<ActionResult<MultiplyOperationResultModel>> MultiplyElementsAsync([FromBody] MultiplyOperationModel factors)
        {
            var multiplyOperationResult = await _calculatorService.ExecuteOperation(_mapper.Map<OperationDTOOperands>(factors), ValidOperations.Mult);

            return Ok(_mapper.Map<MultiplyOperationResultModel>(multiplyOperationResult));
        }

        /// <summary>
        /// Sub two operands and retrieve the result.
        /// </summary>
        /// <param name="subOperators"></param>
        /// <returns></returns>
        [HttpPost("sub")]
        public async Task<ActionResult<SubOperationResultModel>> SubElementsAsync([FromBody] SubOperationModel subOperators)
        {
            var subOperationResult = await _calculatorService.ExecuteOperation(_mapper.Map<OperationDTOOperands>(subOperators), ValidOperations.Diff);

            return Ok(_mapper.Map<SubOperationResultModel>(subOperationResult));
        }

        /// <summary>
        /// Square root for an operand.
        /// </summary>
        /// <param name="sqrtOperators"></param>
        /// <returns></returns>
        [HttpPost("sqrt")]
        public async Task<ActionResult<SqrtOperationResultModel>> SqrtElementsAsync([FromBody] SqrtOperationModel sqrtOperators)
        {
            var sqrtOperationResult = await _calculatorService.ExecuteOperation(_mapper.Map<OperationDTOOperands>(sqrtOperators), ValidOperations.Sqrt);

            return Ok(_mapper.Map<SqrtOperationResultModel>(sqrtOperationResult));
        }

        /// <summary>
        /// Div two operands and retrieve the result.
        /// </summary>
        /// <param name="subOperators"></param>
        /// <returns></returns>
        [HttpPost("div")]
        public async Task<ActionResult<DivOperationResultModel>> DivElementsAsync([FromBody] DivOperationModel sqrtOperators)
        {
            var divOperationResult = await _calculatorService.ExecuteOperation(_mapper.Map<OperationDTOOperands>(sqrtOperators), ValidOperations.Div) ;

            return Ok(_mapper.Map<DivOperationResultModel>(divOperationResult));
        }
    }
}
