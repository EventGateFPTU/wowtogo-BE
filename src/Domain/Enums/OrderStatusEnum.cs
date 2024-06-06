namespace Domain.Enums;
public enum OrderStatusEnum
{
    Canceled = 0,
    Pending = 1,
    Paid = 2,
    Refunded = 3,
}

public static class UserStatusEnumExtensions
{
    public static string ToUserStatusEnumString(this int value)
    {
        return value switch
        {
            (int)OrderStatusEnum.Canceled => "Canceled",
            (int)OrderStatusEnum.Pending => "Pending",
            (int)OrderStatusEnum.Paid => "Paid",
            (int)OrderStatusEnum.Refunded => "Refunded",
            _ => "Canceled",
        };
    }
    public static int ToUserStatusEnumNumber(this string value)
    {
        return value switch
        {
            "Canceled" => (int)OrderStatusEnum.Canceled,
            "Pending" => (int)OrderStatusEnum.Pending,
            "Paid" => (int)OrderStatusEnum.Paid,
            "Refunded" => (int)OrderStatusEnum.Refunded,
            _ => (int)OrderStatusEnum.Canceled,
        };
    }
}