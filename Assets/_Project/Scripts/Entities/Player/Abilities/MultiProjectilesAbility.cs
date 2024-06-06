public class MultiProjectilesAbility : Ability
{
    public override float Use()
    {
        AbilityAudioPlayer.PlayMultiProjectileAbilitySound();
        return ReloadingDuration;
    }

    public override bool CanUse()
    {
        return false;
    }
}
