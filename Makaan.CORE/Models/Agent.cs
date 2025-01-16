using Makaan.CORE.Models.Common;

namespace Makaan.CORE.Models;
public class Agent : BaseEntity
{
    public string FullName { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public int? DesignationId { get; set; }
    public Designation? Designation { get; set; }
}
