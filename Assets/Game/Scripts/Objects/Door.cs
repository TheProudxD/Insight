using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : Interactable
{
    [SerializeField] private DoorType _doorType;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Tilemap _map;

    private void Update()
    {
        if (PlayerInRange)
        {
            if (_doorType is DoorType.Key && _inventory.NumberOfKeys > 0)
            {
                StartCoroutine(OpenRoutine());
            }
        }
    }

    private IEnumerator OpenRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        Context.Raise();
        for (int x = 11; x < 13; x++)
        {
            for (int y = -7; y < -4; y++)
            {
                _map.SetTile(new Vector3Int(x, y, 0), null);
            }
        }

        _inventory.NumberOfKeys--;
        Destroy(gameObject);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerInRange = true;
            Context.Raise();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerInRange = false;
        }
    }
}