using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.Request.Region;

public class UpdateRegionRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int DDD { get; set; }
}
