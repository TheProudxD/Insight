using UnityEngine;

namespace Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        public static PlayerState Current = PlayerState.Walk;
    }
}