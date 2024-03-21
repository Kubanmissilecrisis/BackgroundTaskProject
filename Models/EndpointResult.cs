using System.ComponentModel.DataAnnotations;
namespace WebCheckerAPI.Models;

public class EndpointResult
{

    [Required]
    public Guid EndpointResultId { get; set; }
    [Required]
    public Guid EndpointId { get; set; }
    [Required]
    public DateTime CreatedTime { get; set; }
    [Required]
    public string? State { get; set; }



}
