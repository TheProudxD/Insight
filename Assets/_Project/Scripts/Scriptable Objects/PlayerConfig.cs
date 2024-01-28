using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Scriptable_Objects;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerConfig", fileName = "PlayerConfig", order = 0)]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public FloatValue HpAmount { get; private set; }
    [field: SerializeField] public FloatValue ManaAmount { get; private set; }
    [field: SerializeField] public IntValue Speed { get; private set; }
}