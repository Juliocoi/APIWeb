using Flunt.Notifications;

namespace APIWeb.EndPoints;

public static class ProblemDetailsExtensions
{
    public static Dictionary<string, string[]> ConvertToPromblemDetails(this IReadOnlyCollection<Notification> notifications)
    {
        return notifications
                .GroupBy(g => g.Key) 
                .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
    }
}
