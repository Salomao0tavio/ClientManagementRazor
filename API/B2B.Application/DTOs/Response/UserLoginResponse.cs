using System.Text.Json.Serialization;

namespace B2B.Application.DTOs.Response;

public class UserLoginResponse
{
    public UserLoginResponse()
    {
        Errors = new List<string>();
    }

    public UserLoginResponse(bool sucess, string accessToken) : this()
    {
        AccessToken = accessToken;
    }

    public bool Sucess => Errors.Count == 0;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string AccessToken { get; private set; }

    public List<string> Errors { get; }

    public void AddError(string error)
    {
        Errors.Add(error);
    }

    public void AddErrors(IEnumerable<string> errors)
    {
        Errors.AddRange(errors);
    }
}