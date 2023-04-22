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
      var addOperationResult = await _calculatorService.AddElementsAsync(_mapper.Map<AddOperationDTO>(addends));

      return Ok(_mapper.Map<AddOperationResultModel>(addOperationResult));
    }
  }
}
