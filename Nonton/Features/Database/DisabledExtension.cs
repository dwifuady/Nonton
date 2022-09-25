using System.ComponentModel.DataAnnotations;

namespace Nonton.Features.Database;
public class DisabledExtension
{
    [Key] [Required] public string Id { get; set; } = null!;
    public string AddonId { get; set; } = null!;
    public string TransportUrl { get; set; } = null!;
}
