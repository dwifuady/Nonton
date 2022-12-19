namespace Nonton.Features.Player.Dtos;

public class PlayableItem
{
    public bool IsYoutubeTrailer { get; set; }
    public string? Title { get; set; } = null!;
    public string? ImdbId { get; set; }
    public string Url { get; set; } = null!;
}