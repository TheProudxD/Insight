/*
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageChanger : MonoBehaviour
{
    public GameObject TickRU;
    public GameObject TickEN;
    public GameObject TickES;
    public GameObject TickTR;

    public GameObject NameRU;
    public GameObject NameENG;
    public GameObject NameTR;
    public GameObject NameES;
    public GameObject PhraseRU;
    public GameObject PhraseENG;
    public GameObject PhraseTR;
    public GameObject PhraseES;

    private string _currentLanguage;

    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        _currentLanguage = "en";
        switch (_currentLanguage)
        {
            case "ru":
                SetRussian();
                break;
            case "en":
                SetEnglish();
                break;
            case "tr":
                SetTurkish();
                break;
            case "es":
                SetSpanish();
                break;
            default:
                SetEnglish();
                break;
        }

        ChangeTicks();
        //Debug.Log("Initialization Completed"+ "Available Locales: " + LocalizationSettings.AvailableLocales.Locales.Count);
    }

    public void SetEnglish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        NameENG.SetActive(true);
        NameRU.SetActive(false);
        NameTR.SetActive(false);
        NameES.SetActive(false);

        PhraseENG.SetActive(true);
        PhraseRU.SetActive(false);
        PhraseTR.SetActive(false);
        PhraseES.SetActive(false);
        ChangeTicks();
    }

    public void SetRussian()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        NameENG.SetActive(false);
        NameRU.SetActive(true);
        NameTR.SetActive(false);
        NameES.SetActive(false);

        PhraseENG.SetActive(false);
        PhraseRU.SetActive(true);
        PhraseTR.SetActive(false);
        PhraseES.SetActive(false);
        ChangeTicks();
    }

    public void SetSpanish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
        NameENG.SetActive(false);
        NameRU.SetActive(false);
        NameTR.SetActive(false);
        NameES.SetActive(true);

        PhraseENG.SetActive(false);
        PhraseRU.SetActive(false);
        PhraseTR.SetActive(false);
        PhraseES.SetActive(true);
        ChangeTicks();
    }

    public void SetTurkish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[3];
        NameENG.SetActive(false);
        NameRU.SetActive(false);
        NameTR.SetActive(true);
        NameES.SetActive(false);


        PhraseENG.SetActive(false);
        PhraseRU.SetActive(false);
        PhraseTR.SetActive(true);
        PhraseES.SetActive(false);
        ChangeTicks();
    }

    private void ChangeTicks()
    {
        TickEN.SetActive(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0]);
        TickRU.SetActive(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1]);
        TickES.SetActive(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[2]);
        TickTR.SetActive(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[3]);
    }
}
*/