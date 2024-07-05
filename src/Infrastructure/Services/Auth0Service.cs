using System.Net.Http.Headers;
using System.Net.Http.Json;
using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;



public class Auth0Service(HttpClient httpClient, IConfiguration configuration, IUnitOfWork unitOfWork) : IAuth0Service
{
    private class UserInfoResponse
    {
        public string sub { get; set; }
        public string nickname { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public DateTime updated_at { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }

        public User Map()
            => new()
            {
                Email = email,
                Subject = sub,
                FirstName = nickname
            };
    }
    
    public async Task<User?> SyncUserProfileAsync(string jwt, Guid? updateWithId = null)
    {
        var baseUrl = configuration.GetValue<string>("AUTH0_DOMAIN");
        httpClient.BaseAddress = new Uri($"https://{baseUrl!}");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        var response = await httpClient.PostAsync("/userinfo", null);
        var newUserData = await response.Content.ReadFromJsonAsync<UserInfoResponse>();
        if (newUserData is null) return null;
        
        var user = newUserData.Map();
        user.Id = updateWithId ?? Guid.NewGuid(); // Crete or Update
        user.UpdatedAt = DateTimeOffset.UtcNow;
        unitOfWork.UserRepository.Add(user);
        var result = await unitOfWork.SaveChangesAsync();
        return result ? await unitOfWork.UserRepository.GetUserBySubject(newUserData.sub) : null;
    }
}