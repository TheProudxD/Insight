using Player;
using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    [SerializeField] private GameObject _shopCanvas;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.TryGetComponent(out PlayerAttacking player))
        if (other.CompareTag("Player"))
            _shopCanvas.SetActive(true);    
    }
}
