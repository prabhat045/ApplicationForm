using ApplicationForm.Core.Dto;
using ApplicationForm.Core.Model;
using ApplicationForm.Manager;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace ApplicationForm.Helper
{
    public class ProgramApplicationHelper:IProgramApplicationHelper
    {
        private readonly IProgramApplicationManager _programApplicatioManager;

        public ProgramApplicationHelper(IProgramApplicationManager programApplicationManager) {
        
            _programApplicatioManager = programApplicationManager;

        }

        public async Task<string> CreateProgram(ProgramApplicationDto programApplicationDto)
        {
            var programApplication = new ProgramApplication
            {
                FirstName = new PersonalInfoBase(programApplicationDto.FirstName.IsRequired),
                LastName = new PersonalInfoBase(programApplicationDto.LastName.IsRequired),
                Email = new PersonalInfoBase(programApplicationDto.Email.IsRequired),
                PhoneNumber = new PersonalInfoBase(programApplicationDto.PhoneNumber.Hide, programApplicationDto.PhoneNumber.IsInternal),
                Nationality = new PersonalInfoBase(programApplicationDto.Nationality.Hide, programApplicationDto.Nationality.IsInternal),
                CurrentResidence = new PersonalInfoBase(programApplicationDto.CurrentResidence.Hide, programApplicationDto.CurrentResidence.IsInternal),
                IdNumber = new PersonalInfoBase(programApplicationDto.IdNumber.Hide, programApplicationDto.IdNumber.IsInternal),
                DateOfBirth = new PersonalInfoBase(programApplicationDto.DateOfBirth.Hide, programApplicationDto.DateOfBirth.IsInternal),
                Gender = new PersonalInfoBase(programApplicationDto.Gender.Hide, programApplicationDto.Gender.IsInternal),
                Id = Guid.NewGuid().ToString(),
                Title = programApplicationDto.Title,
                Description = programApplicationDto.Description,
                Question = programApplicationDto.Questions.Select(q => new Question
                {
                    Label = q.Label,
                    Type = q.Type,
                    Options = q.Options,
                    

                }).ToList(),
            };

           return await _programApplicatioManager.CreateProgram(programApplication).ConfigureAwait(false);
           

        }

        public async Task<string> UpdateProgram(ProgramApplicationDto programApplicationDto,string programId)
        {
            var programApplication = new ProgramApplication
            {
                FirstName = new PersonalInfoBase(programApplicationDto.FirstName.IsRequired),
                LastName = new PersonalInfoBase( programApplicationDto.LastName.IsRequired),
                Email = new PersonalInfoBase(programApplicationDto.Email.IsRequired),
                PhoneNumber = new PersonalInfoBase(programApplicationDto.PhoneNumber.Hide, programApplicationDto.PhoneNumber.IsInternal),
                Nationality = new PersonalInfoBase(programApplicationDto.Nationality.Hide, programApplicationDto.Nationality.IsInternal),
                CurrentResidence = new PersonalInfoBase( programApplicationDto.CurrentResidence.Hide, programApplicationDto.CurrentResidence.IsInternal),
                IdNumber = new PersonalInfoBase(programApplicationDto.IdNumber.Hide, programApplicationDto.IdNumber.IsInternal),
                DateOfBirth = new PersonalInfoBase( programApplicationDto.DateOfBirth.Hide, programApplicationDto.DateOfBirth.IsInternal),
                Gender = new PersonalInfoBase(programApplicationDto.Gender.Hide, programApplicationDto.Gender.IsInternal),
                Id = programId,
                Title = programApplicationDto.Title,
                Description = programApplicationDto.Description,
                Question = programApplicationDto.Questions.Select(q => new Question
                {
                    Label = q.Label,
                    Type = q.Type,
                    Options = q.Options,

                }).ToList(),
            };

            return await _programApplicatioManager.UpdateProgram(programApplication,programId).ConfigureAwait(false);

        }


        public async Task<ProgramApplicationDto> GetProgramApplicationById(string programId)
        {
           var programApplication = await _programApplicatioManager.GetProgramApplicationById(programId).ConfigureAwait(false);
            if(programApplication==null)
            {
                throw new ValidationException("Form Not found");
            }
            var programApplicationDto = new ProgramApplicationDto
            {
                FirstName = new PersonalInfoBaseDto(programApplication.FirstName.IsRequired),
                LastName = new PersonalInfoBaseDto(programApplication.LastName.IsRequired),
                Email = new PersonalInfoBaseDto(programApplication.Email.IsRequired),
                PhoneNumber = new PersonalInfoBaseDto(programApplication.PhoneNumber.Hide, programApplication.PhoneNumber.IsInternal),
                Nationality = new PersonalInfoBaseDto(programApplication.Nationality.Hide, programApplication.Nationality.IsInternal),
                CurrentResidence = new PersonalInfoBaseDto(programApplication.CurrentResidence.Hide, programApplication.CurrentResidence.IsInternal),
                IdNumber = new PersonalInfoBaseDto(programApplication.IdNumber.Hide, programApplication.IdNumber.IsInternal),
                DateOfBirth = new PersonalInfoBaseDto(programApplication.DateOfBirth.Hide, programApplication.DateOfBirth.IsInternal),
                Gender = new PersonalInfoBaseDto(programApplication.Gender.Hide, programApplication.Gender.IsInternal),
                Title = programApplication.Title,
                Description = programApplication.Description,
                Questions = programApplication.Question.Select(q => new QuestionDto
                {
                    Label = q.Label,
                    Type = q.Type,
                    Options = q.Options,

                }).ToList(),
            };
            return programApplicationDto;
        }



        
    }
}
