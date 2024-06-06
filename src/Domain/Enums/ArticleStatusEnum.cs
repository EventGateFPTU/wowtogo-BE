namespace Domain.Enums;
public enum ArticleStatusEnum
{
    Canceled = 0,
    Draft = 1,
    Published = 2,
    Completed = 3,
}
public static class ArticleEnumExtensions
{
    public static string ToArticleEnumString(this int value)
    {
        return value switch
        {
            (int)ArticleStatusEnum.Canceled => "Canceled",
            (int)ArticleStatusEnum.Draft => "Draft",
            (int)ArticleStatusEnum.Published => "Published",
            (int)ArticleStatusEnum.Completed => "Completed",
            _ => "Canceled",
        };
    }
    public static int ToArticleEnumNumber(this string value)
    {
        return value switch
        {
            "Canceled" => (int)ArticleStatusEnum.Canceled,
            "Draft" => (int)ArticleStatusEnum.Draft,
            "Published" => (int)ArticleStatusEnum.Published,
            "Completed" => (int)ArticleStatusEnum.Completed,
            _ => (int)ArticleStatusEnum.Draft,
        };
    }
}
