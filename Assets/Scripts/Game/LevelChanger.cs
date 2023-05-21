using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private PlayerController Player;
    [SerializeField] private Animator FadeAnimator;
    [SerializeField] private Image _loadingBar;
    private const string LOBBY = "Lobby";
    private const string LEVEL = "Level";
    public void StopTransition()
    {
        StopAllCoroutines();
        _loadingBar.fillAmount = 0f;
    }
    public void Transition()
    {
        StartCoroutine(LoadingTimer());
    }
    public void OnFadeComplete()
    {
        if (SceneManager.GetActiveScene().name == LOBBY)
            GameManager.Instance.GameLevel = PlayerPrefs.GetInt(LEVEL);
        else
            GameManager.Instance.GameLevel++;

        int level = PlayerPrefs.GetInt(LEVEL);
        if (GameManager.Instance.GameLevel > level)
            PlayerPrefs.SetInt(LEVEL, GameManager.Instance.GameLevel);

        FadeAnimator.SetTrigger("Fade");

        SceneManager.LoadScene(GameManager.Instance.GameLevel);

        PlayerPrefs.Save();
    }
    private IEnumerator LoadingTimer()
    {
        while (_loadingBar.fillAmount < 1f)
        {
            _loadingBar.fillAmount += 0.1f;
            yield return new WaitForSecondsRealtime(0.15f);
        }
        FadeAnimator.SetTrigger("Fade");
        _loadingBar.fillAmount = 0f;
    }
}
