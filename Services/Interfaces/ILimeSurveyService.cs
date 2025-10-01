namespace LimeSurveyAPI.Services.Interfaces
{
    public interface ILimeSurveyService
    {
        Task<string> GetSessionKeyAsync(string username, string password);
        Task<List<Dictionary<string, object>>> GetUsersAsync(string sessionKey);
    }
}
