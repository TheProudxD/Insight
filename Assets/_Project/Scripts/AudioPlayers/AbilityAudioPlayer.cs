using UnityEngine;

public class AbilityAudioPlayer : AudioPlayer
{
    [SerializeField] private AudioSource _dashAbility;
    [SerializeField] private AudioSource _fireCircle;
    [SerializeField] private AudioSource _multiProjectile;

    public void PlayDashAbilitySound() => TryToPlay(_dashAbility);

    public void PlayFireCircleAbilitySound() => TryToPlay(_fireCircle);

    public void PlayMultiProjectileAbilitySound() => TryToPlay(_multiProjectile);
}