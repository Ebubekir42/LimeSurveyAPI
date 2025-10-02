namespace LimeSurveyAPI.Services.Interfaces
{
    public interface ILimeSurveyService
    {
        Task<string> GetSessionKeyAsync(string username, string password);
        Task<List<Dictionary<string, object>>> GetUsersAsync(string sessionKey);
        Task<List<Dictionary<string, object>>> ListSurveysAsync(string sessionKey, string? username = null);
        Task<List<Dictionary<string, object>>> ListUserGroupsAsync(string sessionKey);
        Task<List<Dictionary<string, object>>> ListSurveyGroupsAsync(string sessionKey);
    }
}
