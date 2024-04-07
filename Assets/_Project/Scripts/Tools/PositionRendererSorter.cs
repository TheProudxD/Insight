using UnityEngine;

namespace Utilites
{
    /*
     * Automatically sort a Renderer (SpriteRenderer, MeshRenderer) based on his Y position
     * */
    public class PositionRendererSorter : MonoBehaviour
    {
        [SerializeField]
        private int sortingOrderBase = 5000; // This number should be higher than what any of your sprites will be on the position.y

        [SerializeField] private int _offset;
        [SerializeField] private bool _runOnlyOnce;

        private float _timer;
        private readonly float timerMax = .1f;
        private Renderer _myRenderer;

        private void Awake()
        {
            _myRenderer = gameObject.GetComponent<Renderer>();
        }

        private void LateUpdate()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _timer = timerMax;
                _myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - _offset);
                if (_runOnlyOnce)
                {
                    Destroy(this);
                }
            }
        }

        public void SetOffset(int offset) => _offset = offset;
    }
}