namespace Domain.Enums;
public enum UsedInFormatEnum
{
    Code = 0,
    QR = 1,
}
public static class UsedInFormatEnumExtensions
{
    public static string ToUsedInFormatEnumString(this int value)
    {
        return value switch
        {
            (int)UsedInFormatEnum.Code => UsedInFormatEnum.Code.ToString(),
            (int)UsedInFormatEnum.QR => UsedInFormatEnum.QR.ToString(),
            _ => UsedInFormatEnum.Code.ToString(),
        };
    }
    public static int ToUsedInFormatEnumNumber(this string value)
    {
        return value switch
        {
            "Code" => (int)UsedInFormatEnum.Code,
            "QR" => (int)UsedInFormatEnum.QR,
            _ => (int)UsedInFormatEnum.Code,
        };
    }
}