using Player;
using Zenject;

public class HealingReaction : ItemReaction
{
    [Inject] private PlayerHealth _playerHealth;

    private readonly float _healAmount = 3f;
    
    public override void Use()
    {
        _playerHealth.TryIncrease(_healAmount);
    }
}