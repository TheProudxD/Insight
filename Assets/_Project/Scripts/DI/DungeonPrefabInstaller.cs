using UnityEngine;
using Zenject;

public class DungeonPrefabInstaller : MonoInstaller
{
    [SerializeField] private Animator _enemyDeathEffectAnimator;

    public override void InstallBindings() => 
        Container.Bind<Animator>().WithId("enemyDeathEffect").FromComponentInNewPrefab(_enemyDeathEffectAnimator).AsSingle();
}