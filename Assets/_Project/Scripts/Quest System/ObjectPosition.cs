using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ObjectPosition", fileName = "ObjectPosition", order = 0)]
    public class ObjectPosition : ScriptableObject
    {
        [field: SerializeField] public Vector3[] Positions { get; private set; }

        public int Count => Positions.Length;
    }
}