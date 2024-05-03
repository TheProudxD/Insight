using UnityEngine;
using Utilites;

namespace Extensions
{
    public static class CameraExtensions
    {
        public static Vector3 GetWorldPositionFromUIWithZeroZ(this Camera camera)
        {
            var vec = GetWorldPositionFromUI(camera, Input.mousePosition);
            vec.z = 0f;
            return vec;
        }

        public static Vector3 GetWorldPositionFromUI(this Camera camera) =>
            GetWorldPositionFromUI(camera, Input.mousePosition);

        public static Vector3 GetWorldPositionFromUI(this Camera camera, Vector3 screenPosition) =>
            camera.ScreenToWorldPoint(screenPosition);

        public static Vector3 GetWorldPositionFromUI_Perspective(this Camera camera) =>
            GetWorldPositionFromUI_Perspective(camera, Input.mousePosition);

        public static Vector2 GetWorldUIPosition(this Camera uiCamera, Vector3 worldPosition, Transform parent,
            Camera worldCamera)
        {
            var screenPosition = worldCamera.WorldToScreenPoint(worldPosition);
            var uiCameraWorldPosition = uiCamera.ScreenToWorldPoint(screenPosition);
            var localPos = parent.InverseTransformPoint(uiCameraWorldPosition);
            return new Vector2(localPos.x, localPos.y);
        }

        public static Vector3 GetWorldPositionFromUI_Perspective(this Camera worldCamera, Vector3 screenPosition)
        {
            var ray = worldCamera.ScreenPointToRay(screenPosition);
            var plane = new Plane(Vector3.forward, new Vector3(0, 0, 0f));
            plane.Raycast(ray, out var distance);
            return ray.GetPoint(distance);
        }

        // Get Mouse Position in World with Z = 0f
        public static Vector3 GetMouseWorldPosition(this Camera camera) =>
            GetMouseWorldPosition(camera, Input.mousePosition);

        public static Vector3 GetMouseWorldPosition(this Camera camera, Vector3 position)
        {
            var vec = GetMouseWorldPositionWithZ(camera, position);
            vec.z = 0f;
            return vec;
        }

        public static Vector3 GetMouseWorldPositionWithZ(this Camera camera) =>
            GetMouseWorldPositionWithZ(camera, Input.mousePosition);

        public static Vector3 GetMouseWorldPositionWithZ(this Camera camera, Vector3 screenPosition) =>
            camera.ScreenToWorldPoint(screenPosition);

        public static void ShakeCamera(this Camera camera, float intensity, float timer)
        {
            var lastCameraMovement = Vector3.zero;
            FunctionUpdater.Create(() =>
            {
                timer -= Time.unscaledDeltaTime;
                var randomMovement =
                    new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized *
                    intensity;
                var cameraTransform = camera.transform;
                cameraTransform.position = cameraTransform.position - lastCameraMovement + randomMovement;
                lastCameraMovement = randomMovement;
                return timer <= 0f;
            }, "CAMERA_SHAKE");
        }
    }
}