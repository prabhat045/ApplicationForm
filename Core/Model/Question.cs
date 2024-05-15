using ApplicationForm.Core.Enum;

namespace ApplicationForm.Core.Model
{
    public class Question
    {
        public string Label { get; set; }
        public QuestionType Type { get; set; }
        public List<string> Options { get; set; }
    }
}
