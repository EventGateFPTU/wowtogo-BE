using System.Text.Json.Serialization;
using Domain.Models;

namespace UseCases.Common.Models;

public class UserInfo
{
    [JsonPropertyName("sub")]
    public string Sub { get; set; }
    [JsonPropertyName("nickname")]
    public string Nickname { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("picture")]
    public string Picture { get; set; }
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("email_verified")]
    public bool EmailVerified { get; set; }

    public User Map()
        => new()
        {
            Email = Email,
            Subject = Sub,
            FirstName = Nickname,
        };
}