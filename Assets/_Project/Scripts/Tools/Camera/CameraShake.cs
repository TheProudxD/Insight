using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Shake(0.5f, 2));
    }
    IEnumerator Shake(float duration, float magnitude)
    {
        //yield return new WaitForSeconds(duration);

        float elapsed = 0.0f;

        Vector3 originalCamPos = transform.localPosition;

        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(x, y, originalCamPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalCamPos;
    }
}
