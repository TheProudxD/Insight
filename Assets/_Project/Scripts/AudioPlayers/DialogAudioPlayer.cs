using UnityEngine;

public class DialogAudioPlayer : AudioPlayer
{
    [SerializeField] private AudioSource[] _dialog;
    
    private AudioSource _current;

    public void Play()
    {
        _current = _dialog.GetRandom();
        TryToPlay(_current);
    }

    public void Stop() => _current.Stop();
}