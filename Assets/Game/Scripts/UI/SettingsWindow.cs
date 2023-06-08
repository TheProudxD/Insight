using UnityEngine;
using UnityEngine.Audio;

class SettingsWindow : WindowCommon
{
    [SerializeField] private GameObject _settingsWindow;
    public AudioMixer _music;
    public void OpenSettingsWindow()
    {
        WindowManager.TryShow(WindowType.Settings);
        _settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        if (WindowManager.IsPause) WindowManager.TryClose(WindowType.Settings);
        _settingsWindow.SetActive(false);
    }
    public void AudioVolume(float sliderValue)
    {
        _music.SetFloat("masterVolume", sliderValue);
    }
    public void Quality(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }
}
