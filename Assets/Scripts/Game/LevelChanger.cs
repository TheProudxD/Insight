using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private PlayerController Player;
    [SerializeField] private Animator FadeAnimator;

    private const string LOBBY = "Lobby";
    private const string LEVEL = "Level";
    public void FadeToLevel()
    {
        FadeAnimator.SetTrigger("Fade");
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
}
