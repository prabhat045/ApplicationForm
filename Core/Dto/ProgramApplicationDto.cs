namespace ApplicationForm.Core.Dto
{
    public class ProgramApplicationDto : PersonalInfoDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<QuestionDto> Questions { get; set; }
    }
}
