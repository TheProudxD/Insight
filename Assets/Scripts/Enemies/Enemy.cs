using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Walk,
    Attack,
    Stagger
}

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyState CurrentState;
    [SerializeField] protected Rigidbody2D _enemyRigidbody;
    [SerializeField] protected int _health;
    [SerializeField] protected string _enemyName;
    [SerializeField] protected int _baseAttack;
    [SerializeField] protected float _moveSpeed;
    protected const string XMOVE_STATE = "moveX";
    protected const string YMOVE_STATE = "moveY";
    protected const string WAKEUP_STATE = "wakeUp";

    public IEnumerator KnockCO(float knockTime)
    {
        CurrentState = EnemyState.Stagger;
        yield return new WaitForSeconds(knockTime);
        _enemyRigidbody.velocity = Vector2.zero;
        CurrentState = EnemyState.Idle;
    }
}