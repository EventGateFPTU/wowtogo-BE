namespace Domain.Enums;
public enum EventStatusEnum
{
    Canceled = 0,
    Draft = 1,
    Published = 2,
    Completed = 3,
}

public static class EventEnumExtensions
{
    public static string ToEventEnumString(this int value)
    {
        return value switch
        {
            (int)EventStatusEnum.Canceled => "Canceled",
            (int)EventStatusEnum.Draft => "Draft",
            (int)EventStatusEnum.Published => "Published",
            (int)EventStatusEnum.Completed => "Completed",
            _ => "Canceled",
        };
    }
    public static int ToEventEnumNumber(this string value)
    {
        return value switch
        {
            "Canceled" => (int)EventStatusEnum.Canceled,
            "Draft" => (int)EventStatusEnum.Draft,
            "Published" => (int)EventStatusEnum.Published,
            "Completed" => (int)EventStatusEnum.Completed,
            _ => (int)EventStatusEnum.Draft,
        };
    }
}