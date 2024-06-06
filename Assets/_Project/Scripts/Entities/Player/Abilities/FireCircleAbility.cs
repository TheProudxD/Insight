using System;
using Enemies;
using Extensions;
using Player;
using UnityEngine;

public class FireCircleAbility : Ability
{
    [SerializeField] private ParticleSystem _firePrefab;
    [SerializeField] private float _damageOnEntered;
    [SerializeField] private float _damageStaying;
    [SerializeField] private Collider2D _collider;

    private readonly float _size = 0.08f;

    protected override void Awake()
    {
        base.Awake();
        _collider.enabled = false;
    }
    
    protected override void Update()
    {
        if (ReloadingDurationTimer <= ReloadingDuration)
        {
            ReloadingDurationTimer += Time.deltaTime;
        }
        else
        {
            _collider.enabled = false;
        }
    }
    
    public override float Use()
    {
        ReloadingDurationTimer = 0;
        AbilityAudioPlayer.PlayFireCircleAbilitySound();
        for (int angle = 0; angle < 360; angle++)
        {
            angle += 30;
            var fire = Instantiate(_firePrefab, transform.parent.position + angle.GetVectorFromAngle(),
                Quaternion.identity);
            var mainModule = fire.main;
            mainModule.playOnAwake = false;
            mainModule.duration = Duration;
            fire.Play();
            fire.transform.localScale = new Vector3(_size, _size, _size);
            _collider.enabled = true;
        }

        PlayerStateMachine.Current = PlayerState.Idle;
        return ReloadingDuration;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            enemyHealth.TakeDamage(_damageOnEntered);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            enemyHealth.TakeDamage(_damageStaying);
        }
    }

    public override bool CanUse() => ReloadingDurationTimer >= ReloadingDuration;
}