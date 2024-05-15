using ApplicationForm.Core.Model;

namespace ApplicationForm.Manager
{
    public interface IProgramApplicationManager
    {

        Task<string> CreateProgram(ProgramApplication application);

        Task<string> UpdateProgram(ProgramApplication application, string programId);

        Task<ProgramApplication> GetProgramApplicationById(string programId);
    }
}
