using UnityEngine;

public class LevelResultAudioPlayer : AudioPlayer
{
    [SerializeField] private AudioSource _levelCompleted;
    [SerializeField] private AudioSource _levelFailed;
    
    public void PlayLevelCompleted() => Play(_levelCompleted);

    public void PlayLevelFailed() => Play(_levelFailed);
}