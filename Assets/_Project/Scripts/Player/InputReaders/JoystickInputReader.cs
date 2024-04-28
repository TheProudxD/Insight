using UnityEngine;

namespace Player
{
    public class JoystickInputReader : IInputReader
    {
        private readonly Joystick _joystick;

        public JoystickInputReader(Joystick joystick)
        {
            _joystick = joystick;
            _joystick.gameObject.SetActive(true);
        }
		
        public Vector2 GetInputDirection()
        {
            var horizontalAxis = _joystick.Horizontal;
            var verticalAxis = _joystick.Vertical;
			
            return new Vector2(horizontalAxis, verticalAxis);
        }
    }
}