namespace ApplicationForm.Core.Dto
{
    public class PersonalInfoBaseDto
    {
        public bool IsRequired { get; set; }
        public bool IsInternal { get; set; }

        public bool Hide {  get; set; }

        public PersonalInfoBaseDto() { }
        public PersonalInfoBaseDto(bool hide, bool isInternal)
        {
            Hide = hide;
            IsInternal = isInternal;
        }

        public PersonalInfoBaseDto(bool isRequired)
        {
            IsRequired = isRequired;
        }
    }
}
