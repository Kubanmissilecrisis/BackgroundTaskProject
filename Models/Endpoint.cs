using System.ComponentModel.DataAnnotations;

namespace WebCheckerAPI.Models;

public class Endpoint
{
    [Required]
    public Guid EndpointId { get; set; }

    [Required]
    public string? Url { get; set; }
    [Required]
    public string? Time { get; set; }
}
