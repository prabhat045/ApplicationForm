using ApplicationForm.Core.Dto;

namespace ApplicationForm.Helper
{
    public interface IProgramApplicationHelper
    {
        Task<string> CreateProgram(ProgramApplicationDto programApplicationDto);
        Task<string> UpdateProgram(ProgramApplicationDto programApplicationDto, string programId);

        Task<ProgramApplicationDto> GetProgramApplicationById(string programId);
    }
}
