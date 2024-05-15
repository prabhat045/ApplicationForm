namespace ApplicationForm.Core.Model
{
    public class ResponsePersonalInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public string Nationality { get; set; }

        public string CurrentResidence { get; set; }

        public int IdNumber { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Gender { get; set; }
    }
}
