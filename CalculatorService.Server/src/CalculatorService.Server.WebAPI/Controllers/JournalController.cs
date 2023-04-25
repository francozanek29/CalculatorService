using AutoMapper;
using CalculatorService.Server.Core.Model.Interfaces;
using CalculatorService.Server.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.Server.WebAPI.Controllers
{
    [Route("journal")]
    public class JournalController : Controller
    {
        private readonly IJournalService _journalService;
        private readonly IMapper _mapper;

        public JournalController(IJournalService journalService, IMapper mapper)
        {
            _journalService = journalService;
            _mapper = mapper;
        }


        [HttpPost("query")]
        public async Task<ActionResult<JornalModelResult>> AddElementsAsync([FromBody] JornalModel jornalModel)
        {
            var listOperationJornal= await _journalService.GetJornalOperationByIdAsync(jornalModel.TrackingId);

            return Ok(new JornalModelResult()
            {
                Operations = _mapper.Map<IEnumerable<JornalModelResultItem>>(listOperationJornal)
            });
        }
    }
}
