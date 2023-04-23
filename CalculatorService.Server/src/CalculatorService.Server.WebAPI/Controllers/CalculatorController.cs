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
    public async Task<ActionResult<AddOperationResultModel>> AddElementsAsync([FromBody] AddOperationModel addends,
                                                                              [FromHeader(Name = "X-Evi-Tracking-Id")] string? trackingId)
    {
      var addOperationResult = await _calculatorService.AddElementsAsync(_mapper.Map<OperationDTOOperands>(addends),trackingId);

      return Ok(_mapper.Map<AddOperationResultModel>(addOperationResult));
    }


    [HttpPost("mult")]
    public async Task<ActionResult<MultiplyOperationResultModel>> MultiplyElementsAsync([FromBody] MultiplyOperationModel factors,
                                                                              [FromHeader(Name = "X-Evi-Tracking-Id")] string? trackingId)
    {
      var multiplyOperationResult = await _calculatorService.MultiplyElementsAsync(_mapper.Map<OperationDTOOperands>(factors), trackingId);

      return Ok(_mapper.Map<MultiplyOperationResultModel>(multiplyOperationResult));
    }
  }
}
