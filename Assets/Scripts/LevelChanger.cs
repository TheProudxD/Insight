using UnityEngine;

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
        FadeAnimator.SetTrigger("Fade");
        Player.transform.position = NewLevelPosition.transform.position;
    }
}
