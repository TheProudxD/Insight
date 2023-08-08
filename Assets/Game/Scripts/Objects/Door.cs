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
                Open();
            }
        }
    }

    private void Open()
    {
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
}