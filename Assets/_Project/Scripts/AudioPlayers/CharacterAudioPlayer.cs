using System;
using Player;
using UnityEngine;


public class CharacterAudioPlayer : AudioPlayer
{
    [SerializeField] private AudioSource _playerStepsInner;
    [SerializeField] private AudioSource _playerStepsOuter;
    [SerializeField] private AudioSource _swordAttack;
    [SerializeField] private AudioSource _bowAttack;
    [SerializeField] private AudioSource _pickup;

    public void PlayStepsSound(GroundType groundType)
    {
        switch (groundType)
        {
            case GroundType.Inner:
                TryToPlay(_playerStepsInner);
                break;
            case GroundType.Outer:
                TryToPlay(_playerStepsOuter);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(groundType), groundType, null);
        }
    }

    public void PlayAttack(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Sword:
                TryToPlay(_playerStepsInner);
                _swordAttack.Play();
                break;
            case AttackType.Bow:
                TryToPlay(_bowAttack);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(attackType), attackType, null);
        }
    }

    public void PlayPickupItemSound() => TryToPlay(_pickup);

    public enum GroundType
    {
        Inner,
        Outer
    }
}