using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private PlayerMovement Player;
    [SerializeField] private Animator FadeAnimator;
    public void FadeToLevel()
    {
        FadeAnimator.SetTrigger("Fade");
    }
    public void OnFadeComplete()
    {
        print(FadeAnimator);
        if (SceneManager.GetActiveScene().name == "Lobby")
            GameManager.Instance.GameLevel = PlayerPrefs.GetInt("Level");
        else
            GameManager.Instance.GameLevel++;
        int level = PlayerPrefs.GetInt("Level");

        if (GameManager.Instance.GameLevel > level)
            PlayerPrefs.SetInt("Level", GameManager.Instance.GameLevel);

        FadeAnimator.SetTrigger("Fade");

        SceneManager.LoadScene(GameManager.Instance.GameLevel);

        PlayerPrefs.Save();
    }
}
