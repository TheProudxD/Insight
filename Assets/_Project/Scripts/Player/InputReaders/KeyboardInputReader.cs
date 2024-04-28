using Tools;
using UnityEngine;

namespace Player
{
    public class KeyboardInputReader : IInputReader
    {
        public KeyboardInputReader(Joystick joystick)
        {
            Object.Destroy(joystick.gameObject);
        }

        public Vector2 GetInputDirection()
        {
            var horizontalAxis = Input.GetAxisRaw(Constants.HORIZONTAL_AXIS);
            var verticalAxis = Input.GetAxisRaw(Constants.VERTICAL_AXIS);
            return new Vector2(horizontalAxis, verticalAxis);
        }
    }
}