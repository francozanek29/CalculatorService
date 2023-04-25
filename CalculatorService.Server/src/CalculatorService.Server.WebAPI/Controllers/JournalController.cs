using AutoMapper;
using CalculatorService.Server.Core.Model.Interfaces;
using CalculatorService.Server.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.Server.WebAPI.Controllers
{
    /// <summary>
    /// Controller to handle all the query operation.
    /// </summary>
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

        /// <summary>
        /// Request all operations for a Tracking­Id since the last application restart.
        /// </summary>
        /// <param name="jornalModel"></param>
        /// <returns></returns>
        [HttpPost("query")]
        public async Task<ActionResult<JournalModelResult>> AddElementsAsync([FromBody] JournalModel jornalModel)
        {
            var listOperationJornal = await _journalService.GetJornalOperationByIdAsync(jornalModel.TrackingId);

            return Ok(new JournalModelResult()
            {
                Operations = _mapper.Map<IEnumerable<JournalModelResultItem>>(listOperationJornal)
            });
        }
    }
}
