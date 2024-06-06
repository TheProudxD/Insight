using UnityEngine;

public class BGMAudioPlayer : AudioPlayer
{
    [SerializeField] private AudioSource[] _music;

    public void Play() => TryToPlay(_music.GetRandom());
}