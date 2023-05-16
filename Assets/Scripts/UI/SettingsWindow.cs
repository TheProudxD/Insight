using UnityEngine;
using UnityEngine.Audio;

class SettingsWindow : WindowCommon
{
    public AudioMixer _music;
    public void AudioVolume(float sliderValue)
    {
        _music.SetFloat("masterVolume", sliderValue);
    }
    public void Quality(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }
}
