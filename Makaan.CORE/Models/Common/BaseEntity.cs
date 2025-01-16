namespace Makaan.CORE.Models.Common;
public class BaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedTime { get; set; } = DateTime.Now;
}
