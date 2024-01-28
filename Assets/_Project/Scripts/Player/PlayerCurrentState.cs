using UnityEngine;

namespace Player
{
    public class PlayerCurrentState : MonoBehaviour
    {
        public static PlayerState Current = PlayerState.Walk;
    }
}