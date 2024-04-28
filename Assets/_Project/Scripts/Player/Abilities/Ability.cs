using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [field: SerializeField] public float MagicCost { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }

    public abstract void Use();
}

public class DashAbility : Ability
{
    public override void Use()
    {
        
    }
}
