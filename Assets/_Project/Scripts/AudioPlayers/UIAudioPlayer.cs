using UnityEngine;

public class UIAudioPlayer : AudioPlayer
{
    [SerializeField] private AudioSource _buttonPressed;
    [SerializeField] private AudioSource _togglePressed;

    public void PlayButtonSound() => Play(_buttonPressed);
    
    public void PlayToggleSound() => Play(_togglePressed);
}