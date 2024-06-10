using Domain.Models;

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
            (int)ArticleStatusEnum.Canceled => ArticleStatusEnum.Canceled.ToString(),
            (int)ArticleStatusEnum.Draft => ArticleStatusEnum.Draft.ToString(),
            (int)ArticleStatusEnum.Published => ArticleStatusEnum.Published.ToString(),
            (int)ArticleStatusEnum.Completed => ArticleStatusEnum.Completed.ToString(),
            _ => ArticleStatusEnum.Draft.ToString(),
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

    public static string fuckyou(){
        int number = 1;
        return number.ToArticleEnumString();
    }
}
