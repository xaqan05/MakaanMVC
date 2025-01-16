using Makaan.CORE.Models.Common;

namespace Makaan.CORE.Models;
public class Designation : BaseEntity
{
    public string Name { get; set; } = null!;
    public IEnumerable<Agent> Agents { get; set; } = null!;
}
