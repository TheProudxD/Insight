using UnityEngine;
using Zenject;

public abstract class Ability : MonoBehaviour
{
    [Inject] protected AbilityAudioPlayer AbilityAudioPlayer;
    
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
    
    public abstract float Use(); // returns duration
    public abstract bool CanUse();
}