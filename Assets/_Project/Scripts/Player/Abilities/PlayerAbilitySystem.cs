using Objects;
using Player;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMana))]
public class PlayerAbilitySystem : MonoBehaviour
{
    [SerializeField] private DashAbility _dashAbility;

    private PlayerMana _playerMana;

    private void Awake()
    {
        _playerMana = GetComponent<PlayerMana>();
    }

    private bool EnoughMana(Ability ability) => ability.MagicCost >= _playerMana.Amount;

    public float UseDash() // небольшой буст
    {
        if (EnoughMana(_dashAbility) == false || _dashAbility.CanUse() == false)
            return 0;

        PlayerStateMachine.Current = PlayerState.Ability;
        return _dashAbility.Use();
    }

    public float UseMultiProjectile() // выстрел из нескольких снарядов
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }

    public float UseShield() // полное поглощение всего дамага
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