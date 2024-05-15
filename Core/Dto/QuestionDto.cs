using ApplicationForm.Core.Enum;

namespace ApplicationForm.Core.Dto
{
    public class QuestionDto
    {
        public string Label { get; set; }
        public QuestionType Type { get; set; }
        public List<string> Options { get; set; }
    }
}
