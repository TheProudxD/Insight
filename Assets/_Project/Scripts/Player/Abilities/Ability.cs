using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [field: SerializeField] public float MagicCost { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public float ReloadingDuration { get; private set; }
    
    protected float ReloadingDurationTimer;

    protected virtual void Awake() => ReloadingDurationTimer = ReloadingDuration;
    
    protected virtual void Update()
    {
        if (ReloadingDurationTimer <= ReloadingDuration)
        {
            ReloadingDurationTimer += Time.deltaTime;
        }
    }
    
    public abstract float Use(); // return duration
    public abstract bool CanUse();
}