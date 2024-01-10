using Player;
using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    [SerializeField] private GameObject _shopCanvas;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
            _shopCanvas.SetActive(true);    
    }
}
