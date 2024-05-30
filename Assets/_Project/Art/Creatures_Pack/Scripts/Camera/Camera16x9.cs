using UnityEngine;

public class Camera16x9 : MonoBehaviour
{
    private void Start()
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        const float targetaspect = 16.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float rectHeight = windowAspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera cam = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (rectHeight < 1.0f)
        {
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = rectHeight;
            rect.x = 0;
            rect.y = (1.0f - rectHeight) / 2.0f;

            cam.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / rectHeight;

            Rect rect = cam.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }
}