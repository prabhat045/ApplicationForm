using ApplicationForm.Core.Dto;
using ApplicationForm.Core.Model;
using ApplicationForm.Helper;
using ApplicationForm.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {

        private readonly IProgramApplicationHelper _programApplicationHelper;
        private readonly IResponseHelper _responseHelper;
        public ResponseController(IProgramApplicationHelper programApplicationHelper,IResponseHelper responseHelper) { 
            _programApplicationHelper = programApplicationHelper;
            _responseHelper = responseHelper;
        }

        [HttpGet("GetApplication")]
        public async Task<IActionResult> GetApplicationForm (string ProgramId)
        {
            try
            {
                return Ok(await _programApplicationHelper.GetProgramApplicationById(ProgramId).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitApplication(ResponseDto responseDto)
        {
            try
            {
                await _responseHelper.SubmitResponse(responseDto).ConfigureAwait(false);
                return Ok( new {Message = "Response submitted successfully"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
    }
}
