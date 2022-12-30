using Nonton.Features.Player.Dtos;

namespace Nonton.Commons;

public class PlayableItemStateContainer
{
    private PlayableItem? _playableItem;

    public PlayableItem? PlayableItem
    {
        get => _playableItem ?? null;
        set
        {
            _playableItem = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;
    private void NotifyStateChanged() => OnChange?.Invoke();
}