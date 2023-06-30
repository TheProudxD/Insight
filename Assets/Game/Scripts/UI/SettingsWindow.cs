using UnityEngine;
using UnityEngine.Audio;

internal class SettingsWindow : WindowCommon
{
    [SerializeField] private AudioMixer _music;
    private bool _isFullScreen;

    public void AudioVolume(float sliderValue)
    {
        _music.SetFloat("masterVolume", sliderValue);
    }

    public void ChangeQuality(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }

    public void FullScreenToggle()
    {
        _isFullScreen = !_isFullScreen;
        Screen.fullScreen = _isFullScreen;
    }
}