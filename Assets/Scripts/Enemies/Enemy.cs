using UnityEngine;
public enum EnemyState
{
    Idle,
    Walk,
    Attack,
    Stagger
} 

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyState CurrentState;
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected int _health;
    [SerializeField] protected string _enemyName;
    [SerializeField] protected int _baseAttack;
    [SerializeField] protected float _moveSpeed;
}
