using Domain.Enums;
using Domain.Models.Shared;
namespace Domain.Models;
public class Article : DurationEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Status { get; set; } = (int)ArticleStatusEnum.Draft;
}