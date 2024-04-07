using UnityEngine;

public class Randomizer : MonoBehaviour
{
    [ContextMenu("Randomize object")]
    public void Randomize()
    {
        var newRotX = Random.Range(-1, 1f);
        var newRotY = Random.Range(0, 360f);
        var newRotZ = Random.Range(-1, 1f);

        var rot = Quaternion.Euler(newRotX, newRotY, newRotZ);
        transform.rotation = rot;
        transform.localScale = Random.Range(0.1f, 4f) * Vector3.one;
    }

    public void Reset()
    {
        var thisTransform = transform;
        thisTransform.rotation = Quaternion.identity;
        thisTransform.localScale = Vector3.one;
    }
}