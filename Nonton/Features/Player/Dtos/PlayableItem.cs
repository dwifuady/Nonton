using Nonton.Features.Addons.Dtos;

namespace Nonton.Features.Player.Dtos;

public class PlayableItem
{
    public bool IsYoutubeTrailer { get; set; }
    public string? Title { get; set; }
    public string? ImdbId { get; set; }
    public string Url { get; set; } = null!;
    public List<Subtitle>? Subtitles { get; set; }
    public string? Poster { get; set; }
}