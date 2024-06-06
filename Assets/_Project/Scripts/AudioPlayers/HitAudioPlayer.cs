using UnityEngine;

public class HitAudioPlayer : AudioPlayer
{
    [SerializeField] private AudioSource[] _hit;
    
    public void Play() => TryToPlay(_hit.GetRandom());
}