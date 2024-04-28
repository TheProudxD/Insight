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

    public void UseDash() // небольшой буст
    {
        if (EnoughMana(_dashAbility) == false || _dashAbility.CanUse() == false)
            return;

        PlayerStateMachine.Current = PlayerState.Ability;
        _dashAbility.Use();
    }

    public void UseMultiProjectile() // выстрел из нескольких снарядов
    {
        throw new System.NotImplementedException();
    }

    public void UseFireBall() // поджигает при попадании, наносит дамаг в течение времени
    {
        throw new System.NotImplementedException();
    }

    public void UseIceBall() // замедляет при попадании, дамаг при попаднии
    {
        throw new System.NotImplementedException();
    }

    public void UseBoomerang() // пуля возврщается, не ломается при попадании, наносит слабый урон
    {
        throw new System.NotImplementedException();
    }

    public void UseHookshot() // притягивается к ближайжему противнику и наносит смертельный дамаг
    {
        throw new System.NotImplementedException();
    }

    public void UseFireCircle() // огонь в радиусе от игрока
    {
        throw new System.NotImplementedException();
    }

    public void UseShield() // полное поглощение всего дамага
    {
        throw new System.NotImplementedException();
    }

    public void UseBomb() // наступает - взрывается, наносит урон в радиусе
    {
        throw new System.NotImplementedException();
    }

    public void UseBugNet() // наступает - замедлеятся
    {
        throw new System.NotImplementedException();
    }

    public void UseTurret() // ставиться временная турель которая в течение времени стреляет по определенному радиусу
    {
        throw new System.NotImplementedException();
    }
}