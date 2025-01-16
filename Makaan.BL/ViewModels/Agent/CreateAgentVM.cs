using Microsoft.AspNetCore.Http;

namespace Makaan.BL.ViewModels.Agent;

public class CreateAgentVM
{
    public string FullName { get; set; } = null!;
    public IFormFile Image { get; set; } = null!;
    public int? DesignationId { get; set; }
}
