using System.Text.Json.Serialization;


namespace Application.B2B_Application.DTOs.Response
{
    public class UserLoginResponse
    {
        public bool Sucess => Errors.Count == 0;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string AccessToken { get; private set; }
       
        public List<string> Errors { get; private set; }

        public UserLoginResponse() =>
            Errors = new List<string>();

        public UserLoginResponse(bool sucess, string accessToken) : this()
        {
            AccessToken = accessToken;
        }

        public void AddError(string error) =>
            Errors.Add(error);

        public void AddErrors(IEnumerable<string> errors) =>
            Errors.AddRange(errors);
    }
}
