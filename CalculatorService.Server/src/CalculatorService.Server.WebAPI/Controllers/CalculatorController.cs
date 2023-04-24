using AutoMapper;
using CalculatorService.Server.Core.Model.Entitites;
using CalculatorService.Server.Core.Model.Interfaces;
using CalculatorService.Server.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.Server.WebAPI.Controllers
{
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

    [HttpPost("add")]
    public async Task<ActionResult<AddOperationResultModel>> AddElementsAsync([FromBody] AddOperationModel addends)
    {
      var addOperationResult = await _calculatorService.AddElementsAsync(_mapper.Map<OperationDTOOperands>(addends));

      return Ok(_mapper.Map<AddOperationResultModel>(addOperationResult));
    }


    [HttpPost("mult")]
    public async Task<ActionResult<MultiplyOperationResultModel>> MultiplyElementsAsync([FromBody] MultiplyOperationModel factors)
    {
      var multiplyOperationResult = await _calculatorService.MultiplyElementsAsync(_mapper.Map<OperationDTOOperands>(factors));

      return Ok(_mapper.Map<MultiplyOperationResultModel>(multiplyOperationResult));
    }

    [HttpPost("sub")]
    public async Task<ActionResult<SubOperationResultModel>> SubElementsAsync([FromBody] SubOperationModel subOperators)
    {
      var subOperationResult = await _calculatorService.SubElementsAsync(_mapper.Map<OperationDTOOperands>(subOperators));

      return Ok(_mapper.Map<SubOperationResultModel>(subOperationResult));
    }

    [HttpPost("sqrt")]
    public async Task<ActionResult<SqrtOperationResultModel>> SqrtElementsAsync([FromBody] SqrtOperationModel sqrtOperators)
    {
      var sqrtOperationResult = await _calculatorService.SqrtElementsAsync(_mapper.Map<OperationDTOOperands>(sqrtOperators));

      return Ok(_mapper.Map<SqrtOperationResultModel>(sqrtOperationResult));
    }

    [HttpPost("div")]
    public async Task<ActionResult<DivOperationResultModel>> DivElementsAsync([FromBody] DivOperationModel sqrtOperators)
    {
      var divOperationResult = await _calculatorService.DivElementsAsync(_mapper.Map<OperationDTOOperands>(sqrtOperators));

      return Ok(_mapper.Map<DivOperationResultModel>(divOperationResult));
    }
  }
}
