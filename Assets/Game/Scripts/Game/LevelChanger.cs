using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    private const string CONDITION_TO_NEW_LEVEL = "FadeNewLevel";
    [SerializeField] private Animator FadeAnimator;
    [SerializeField] private Image _loadingBar;

    public void StopTransition()
    {
        StopAllCoroutines();
        _loadingBar.fillAmount = 0f;
    }

    public void StartTransition()
    {
        StartCoroutine(LoadingTimer());
    }

    public void OnFadeComplete()
    {
        GameManager.Instance.SaveData();
        FadeAnimator.SetTrigger(CONDITION_TO_NEW_LEVEL);
        SceneManager.LoadScene(GameManager.Instance.GameLevel);
    }

    private IEnumerator LoadingTimer()
    {
        while (_loadingBar.fillAmount < 1f)
        {
            _loadingBar.fillAmount += 0.1f;
            yield return new WaitForSecondsRealtime(0.15f);
        }
        FadeAnimator.SetTrigger(CONDITION_TO_NEW_LEVEL);
        _loadingBar.fillAmount = 0f;
    }

    public void FadeGameOver()
    {
        FadeAnimator.SetTrigger("FadeGameOver");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            StartTransition();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            StopTransition();
        }
    }
}
