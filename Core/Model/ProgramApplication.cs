using Newtonsoft.Json;

namespace ApplicationForm.Core.Model
{
    public class ProgramApplication : PersonalInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public List<Question> Question { get; set; }
    }
}
