using UnityEngine;

public abstract class AudioPlayer : MonoBehaviour
{
    protected void TryToPlay(AudioSource audio)
    {
        if (audio.isPlaying == false)
            audio.Play();
    }

    protected void Play(AudioSource audio)
    {
        audio.Play();
    }
}