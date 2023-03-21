using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LevelChanger _lvlChanger;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Teleport"))
        {
            _lvlChanger.FadeToLevel();
        }

    }
}
