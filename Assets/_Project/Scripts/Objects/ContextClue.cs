using UnityEngine;

namespace Objects
{
    public class ContextClue : MonoBehaviour
    {
        [SerializeField] private GameObject _context;

        public void Enable() => _context.SetActive(true);
        public void Disable() => _context.SetActive(false);

        public void ChangeContext()
        {
            _context.SetActive(!_context.activeSelf);
        }
    }
}