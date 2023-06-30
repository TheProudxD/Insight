using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    private const string ConditionToNewLevel = "FadeNewLevel";

    [FormerlySerializedAs("FadeAnimator")] [SerializeField]
    private Animator _fadeAnimator;

    [SerializeField] private Image _loadingBar;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player)) StartTransition();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player)) StopTransition();
    }

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
        _fadeAnimator.SetTrigger(ConditionToNewLevel);
        SceneManager.LoadScene(GameManager.Instance.GameLevel);
    }

    private IEnumerator LoadingTimer()
    {
        while (_loadingBar.fillAmount < 1f)
        {
            _loadingBar.fillAmount += 0.1f;
            yield return new WaitForSecondsRealtime(0.15f);
        }

        _fadeAnimator.SetTrigger(ConditionToNewLevel);
        _loadingBar.fillAmount = 0f;
    }

    public void FadeGameOver()
    {
        _fadeAnimator.SetTrigger("FadeGameOver");
    }
}