using System.Collections;
using Tools;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Objects
{
    public class Door : Interactable
    {
        [SerializeField] private DoorType _doorType;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Tilemap _map;

        private void Update()
        {
            if (!PlayerInRange) return;
            if (_doorType is not DoorType.Key || _inventory.NumberOfKeys <= 0) return;
            Context.Raise();
            Open();
        }

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

            _inventory.NumberOfKeys--;
            Destroy(gameObject);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (Utils.IsItPlayer(other) == false)
                return;
            
            PlayerInRange = true;
            Context.Raise();
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            if (Utils.IsItPlayer(other) == false)
                return;
            
            PlayerInRange = false;
            Context.Raise();
        }
    }
}