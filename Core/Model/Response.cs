using Newtonsoft.Json;

namespace ApplicationForm.Core.Model
{
    public class Response : ResponsePersonalInfo
    {
        public Response() { }

        [JsonProperty("id")]
        public string Id { get; set; }

        public string programApplicationId { get; set; }

        public List<ResponseAnswers> Answers { get; set; }

    }
}
