using System.ComponentModel.DataAnnotations;

namespace Nonton.Data
{
    public class NontonExtension
    {
        [Key] public string Id { get; set; } = null!;
        public string Manifest { get; set; } = null!;
        public string TransportUrl { get; set; } = null!;
        public bool Enabled { get; set; }
    }
}
