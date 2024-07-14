using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Shared;

namespace Domain.Models;

public class ShowStaff : BaseEntity
{
    public required Guid ShowId { get; set; }
    public required Guid StaffId { get; set; }
    //-----------------------------------------
    [ForeignKey(nameof(ShowId))]
    public Show Show { get; set; } = null!;
    [ForeignKey(nameof(StaffId))]
    public Staff Staff { get; set; } = null!;
}