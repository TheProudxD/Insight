using UnityEngine;

namespace Player
{
    public interface IInputReader
    {
        public Vector2 GetInputDirection();
    }
}