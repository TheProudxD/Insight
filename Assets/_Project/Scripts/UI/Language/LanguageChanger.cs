using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageChanger : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
    }

    private void PickLocale(string localeID) => LocalizationSettings.SelectedLocale =
        LocalizationSettings.AvailableLocales.Locales.First(x => x.Identifier == localeID);

    private void PickLocale(int localeID) => LocalizationSettings.SelectedLocale =
        LocalizationSettings.AvailableLocales.Locales.First(x => x.SortOrder == localeID);

    public void SwitchLanguage(int langID) => PickLocale(langID);

    public void SwitchLanguage(string langID) => PickLocale(langID);

    public void SetEnglish() => PickLocale("en");

    public void SetRussian() => PickLocale("ru");

    public void SetSpanish() => PickLocale("es");

    public void SetGerman() => PickLocale("de");
    
    public void SetFrench() => PickLocale("fr");
}