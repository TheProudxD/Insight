using UnityEngine;
using UnityEngine.Audio;

class SettingsWindow : WindowCommon
{
    private bool _isFullScreen;
    [SerializeField] private AudioMixer _music;

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
