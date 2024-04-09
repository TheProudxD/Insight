using System.Collections;
using Tools;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Objects
{
    public class Door : Interactable
    {
        [SerializeField] private PlayerInventory _inventory;
        [SerializeField] private InventoryItem _keyItem;
        [SerializeField] private DoorType _doorType;
        [SerializeField] private Tilemap _map;

        public void Open() => StartCoroutine(OpenRoutine());

        private IEnumerator OpenRoutine()
        {
            yield return new WaitForSeconds(1.5f);

            for (int x = 11; x < 13; x++)
            {
                for (int y = -7; y < -4; y++)
                {
                    _map.SetTile(new Vector3Int(x, y, 0), null);
                }
            }

            Destroy(gameObject);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (InsightUtils.IsItPlayer(other) == false)
                return;

            if (_doorType is not DoorType.Key) 
                return;
            
            if (_inventory.InventoryItems.Contains(_keyItem) == false) 
                return;
            
            Open();
            
            PlayerInRange = true;
            Context.Raise();
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            if (InsightUtils.IsItPlayer(other) == false)
                return;
            
            PlayerInRange = false;
            Context.Raise();
        }
    }
}