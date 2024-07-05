using System.Net.Http.Headers;
using System.Net.Http.Json;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using UseCases.Common.Contracts;
using UseCases.Common.Models;
using UseCases.Common.Shared;

namespace Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly HttpClient _httpClient;
    private readonly PipelineContext _pipelineContext;
    public CurrentUserService(HttpClient httpClient, IOptionsMonitor<Auth0Settings> auth0SettingsOptions, PipelineContext pipelineContext)
    {
        _httpClient = httpClient;
        _pipelineContext = pipelineContext;
        var auth0Settings = auth0SettingsOptions.CurrentValue;
        var baseUrl = auth0Settings.Domain;
        Console.WriteLine(baseUrl);
        _httpClient.BaseAddress = new Uri($"https://{baseUrl}");
    }
    public async Task<UserInfo?> GetUser()
    {
        var jwt = _pipelineContext.Items["JWT"] as string;
        if (string.IsNullOrEmpty(jwt)) return null;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        var response = await _httpClient.PostAsync("/userinfo", null);
        var newUserData = await response.Content.ReadFromJsonAsync<UserInfo>();

        return newUserData;
    }
}