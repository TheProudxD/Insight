using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private PlayerController Player;
    [SerializeField] private Animator FadeAnimator;
    [SerializeField] private Image _loadImage;
    private bool isLoadPause;
    private const string LOBBY = "Lobby";
    private const string LEVEL = "Level";
    public void FadeToLevel()
    {
        StartCoroutine(LoadingTimer());
    }
    private void Update()
    {
        
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
        _loadImage.fillAmount =1;
        yield return new WaitForSeconds(2);
        _loadImage.fillAmount = 0;
        FadeAnimator.SetTrigger("Fade");
    }
}
