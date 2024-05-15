using ApplicationForm.Core.Dto;
using ApplicationForm.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramApplicationController : ControllerBase
    {
        private readonly IProgramApplicationHelper _programApplicationHelper;

        public ProgramApplicationController(IProgramApplicationHelper programApplicationHelper)
        {
            _programApplicationHelper = programApplicationHelper;
        }

        [HttpPost("CreateApplication")]
        public async Task<string> CreateProgram(ProgramApplicationDto programApplicationDto)
        {
           return await _programApplicationHelper.CreateProgram(programApplicationDto).ConfigureAwait(false);
        }

        [HttpPut("UpdateApplication")]
        public async Task<string> UpdateProgram(ProgramApplicationDto programApplicationDto,string programId)
        {
           return await _programApplicationHelper.UpdateProgram(programApplicationDto,programId).ConfigureAwait(false);
            
        }


    }
}
