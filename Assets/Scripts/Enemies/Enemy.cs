using System.Collections;
using System.Collections.Generic;
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
    public IEnumerator KnockCO(float knockTime)
    {
        yield return new WaitForSeconds(knockTime);
        _enemyRigidbody.velocity = Vector2.zero;
        CurrentState = EnemyState.Idle;
    }
}