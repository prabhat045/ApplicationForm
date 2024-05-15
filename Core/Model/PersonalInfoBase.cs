namespace ApplicationForm.Core.Model
{
    public class PersonalInfoBase
    {

        public bool IsRequired { get; set; }
        public bool IsInternal { get; set; }

        public bool Hide {  get; set; }
        
        public PersonalInfoBase() { }

        public PersonalInfoBase( bool hide, bool isInternal)
        {
            Hide = hide;
            IsInternal = isInternal;
        }

        public PersonalInfoBase( bool isRequired)
        {

           
            IsRequired = isRequired;

        }
    }
}
