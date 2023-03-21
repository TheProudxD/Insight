using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private PlayerMovement Player;
    [SerializeField] private Animator FadeAnimator;
    [SerializeField] private GameObject NewLevelPosition;
    public void FadeToLevel()
    {
        FadeAnimator.SetTrigger("Fade");
    }
    public void OnFadeComplete()
    {
        int level = PlayerPrefs.GetInt("Level");
        FadeAnimator.SetTrigger("Fade");

        if (SceneManager.GetActiveScene().name!="Menu" && SceneManager.GetActiveScene().name != "Lobby")
            PlayerPrefs.SetInt("Level", ++level);
        else
            PlayerPrefs.SetInt("Level", 2);

        SceneManager.LoadScene(level);
    }
}
