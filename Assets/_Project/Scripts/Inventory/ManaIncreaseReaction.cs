using Player;
using Zenject;

public class ManaIncreaseReaction : ItemReaction
{
    [Inject] private PlayerMana _playerMana;

    private readonly float _amount = 3f;

    public override void Use()
    {
        _playerMana.TryIncrease(_amount);
    }
}