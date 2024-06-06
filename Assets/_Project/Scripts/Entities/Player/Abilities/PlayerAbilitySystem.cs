using Player;
using UnityEngine;

[RequireComponent(typeof(PlayerMana))]
public class PlayerAbilitySystem : MonoBehaviour
{
    [SerializeField] private DashAbility _dashAbility;
    [SerializeField] private FireCircleAbility _fireCircleAbility;
    [SerializeField] private MultiProjectilesAbility _multiProjectilesAbility;

    private PlayerMana _playerMana;

    private void Awake() => _playerMana = GetComponent<PlayerMana>();

    private bool EnoughMana(Ability ability) => ability.MagicCost <= _playerMana.Amount;

    private bool TryUseAbility(Ability ability, out float duration)
    {
        if (EnoughMana(ability) == false || ability.CanUse() == false)
        {
            duration = 0;
            return false;
        }

        _playerMana.Decrease(ability.MagicCost);
        PlayerStateMachine.Current = PlayerState.Ability;
        duration = ability.Use();
        return true;
    }

    public float UseDash() // небольшой буст
    {
        TryUseAbility(_dashAbility, out var duration);
        return duration;
    }

    public float UseMultiProjectile() // выстрел из нескольких снарядов
    {
        TryUseAbility(_multiProjectilesAbility, out var duration);
        return duration;
    }

    public float UseFireBall() // поджигает при попадании, наносит дамаг в течение времени
    {
        throw new System.NotImplementedException();
    }

    public float UseIceBall() // замедляет при попадании, дамаг при попаднии
    {
        throw new System.NotImplementedException();
    }

    public float UseBoomerang() // пуля возврщается, не ломается при попадании, наносит слабый урон
    {
        throw new System.NotImplementedException();
    }

    public float UseHookshot() // притягивается к ближайжему противнику и наносит смертельный дамаг
    {
        throw new System.NotImplementedException();
    }

    public float UseFireCircle() // огонь в радиусе от игрока
    {
        TryUseAbility(_fireCircleAbility, out var duration);
        return duration;
    }

    public float UseShield() // полное поглощение всего дамага на некоторое время
    {
        throw new System.NotImplementedException();
    }

    public float UseBomb() // наступает - взрывается, наносит урон в радиусе
    {
        throw new System.NotImplementedException();
    }

    public float UseBugNet() // наступает - замедлеятся
    {
        throw new System.NotImplementedException();
    }

    public float UseTurret() // ставиться временная турель которая в течение времени стреляет по определенному радиусу
    {
        throw new System.NotImplementedException();
    }
}