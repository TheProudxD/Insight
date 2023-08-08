using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    protected const string X_MOVE_STATE = "moveX";
    protected const string Y_MOVE_STATE = "moveY";
    protected const string WAKEUP_STATE = "wakeUp";

    [FormerlySerializedAs("_maxHealth"),SerializeField] protected FloatValue MaxHealth;
    [FormerlySerializedAs("_enemyName"),SerializeField] protected string EnemyName;
    [FormerlySerializedAs("_baseAttack"),SerializeField] protected int BaseAttack;
    [FormerlySerializedAs("_moveSpeed"), SerializeField] protected float MoveSpeed;
    
    protected EnemyState CurrentState { get; private set; }
    protected Rigidbody2D EnemyRigidbody;
    protected Animator Animator;
    protected Transform Target;
    
    private float _health;

    protected void Awake()
    {
        EnemyRigidbody = GetComponent<Rigidbody2D>();
        Target = FindObjectOfType<PlayerController>().transform;
        Animator = GetComponent<Animator>();
        Animator.SetBool(WAKEUP_STATE, true);
    }

    protected void Start()
    {
        if (MaxHealth is null)
            throw new Exception(nameof(MaxHealth));
        _health = MaxHealth.RuntimeValue;
        CurrentState = EnemyState.Idle;
    }

    public IEnumerator KnockCO(float knockTime)
    {
        CurrentState = EnemyState.Stagger;
        yield return new WaitForSeconds(knockTime);
        EnemyRigidbody.velocity = Vector2.zero;
        CurrentState = EnemyState.Idle;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0) gameObject.SetActive(false);
    }

    protected void ChangeState(EnemyState newState)
    {
        CurrentState = newState;
    }
}