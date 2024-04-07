using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Utilites
{
    public static class Utils
    {
        public const int SORTING_ORDER_DEFAULT = 5000;
        
        private static Camera _camera;

        public static Camera Camera
        {
            get
            {
                if (_camera == null)
                    _camera = Camera.main;

                return _camera;
            }
            set => _camera = value;
        }

        // Get Sorting order to set SpriteRenderer sortingOrder, higher position = lower sortingOrder
        public static int GetSortingOrder(Vector3 position, int offset, int baseSortingOrder = SORTING_ORDER_DEFAULT) =>
            (int)(baseSortingOrder - position.y) + offset;

        private static Canvas _canvas;

        public static Canvas GetCanvas()
        {
            if (_canvas == null)
                _canvas = Object.FindObjectOfType<Canvas>();

            return _canvas;
        }


        // Get Default Unity Font, used in text objects if no font given
        public static Font GetDefaultFont() => Resources.GetBuiltinResource<Font>("Arial.ttf");

        // Create a Sprite in the World, no parent
        public static GameObject CreateWorldSprite(string name, Sprite sprite, Vector3 position, Vector3 localScale,
            int sortingOrder, Color color) =>
            CreateWorldSprite(null, name, sprite, position, localScale, sortingOrder, color);

        // Create a Sprite in the World
        public static GameObject CreateWorldSprite(Transform parent, string name, Sprite sprite, Vector3 localPosition,
            Vector3 localScale, int sortingOrder, Color color)
        {
            var gameObject = new GameObject(name, typeof(SpriteRenderer));
            var transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            transform.localScale = localScale;
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = sortingOrder;
            spriteRenderer.color = color;
            return gameObject;
        }

        // Create Text in the World
        public static TextMesh CreateWorldText(string text, Transform parent = null,
            Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null,
            TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left,
            int sortingOrder = SORTING_ORDER_DEFAULT)
        {
            if (color == null) color = Color.white;
            return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment,
                sortingOrder);
        }

        // Create Text in the World
        public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize,
            Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
        {
            var gameObject = new GameObject("World_Text", typeof(TextMesh));
            var transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            var textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }


        // Create a Text Popup in the World, no parent
        public static void CreateWorldTextPopup(string text, Vector3 localPosition) =>
            CreateWorldTextPopup(null, text, localPosition, 40, Color.white, localPosition + new Vector3(0, 20), 1f);

        // Create a Text Popup in the World
        public static void CreateWorldTextPopup(Transform parent, string text, Vector3 localPosition, int fontSize,
            Color color, Vector3 finalPopupPosition, float popupTime)
        {
            var textMesh = CreateWorldText(parent, text, localPosition, fontSize, color, TextAnchor.LowerLeft,
                TextAlignment.Left, SORTING_ORDER_DEFAULT);
            var transform = textMesh.transform;
            var moveAmount = (finalPopupPosition - localPosition) / popupTime;
            FunctionUpdater.Create(() =>
            {
                transform.position += moveAmount * Time.deltaTime;
                popupTime -= Time.deltaTime;
                if (popupTime > 0f) return false;
                Object.Destroy(transform.gameObject);
                return true;
            }, "WorldTextPopup");
        }

        // Create Text Updater in UI
        public static FunctionUpdater CreateUITextUpdater(Func<string> GetTextFunc, Vector2 anchoredPosition)
        {
            var text = DrawTextUI(GetTextFunc(), anchoredPosition, 20, GetDefaultFont());
            return FunctionUpdater.Create(() =>
            {
                text.text = GetTextFunc();
                return false;
            }, "UITextUpdater");
        }


        // Draw a UI Sprite
        public static RectTransform DrawSprite(Color color, Transform parent, Vector2 pos, Vector2 size,
            string name = null)
        {
            var rectTransform = DrawSprite(null, color, parent, pos, size, name);
            return rectTransform;
        }

        // Draw a UI Sprite
        public static RectTransform DrawSprite(Sprite sprite, Transform parent, Vector2 pos, Vector2 size,
            string name = null)
        {
            var rectTransform = DrawSprite(sprite, Color.white, parent, pos, size, name);
            return rectTransform;
        }

        // Draw a UI Sprite
        public static RectTransform DrawSprite(Sprite sprite, Color color, Transform parent, Vector2 pos, Vector2 size,
            string name = null)
        {
            if (name is null or "") 
                name = "Sprite";
            var go = new GameObject(name, typeof(RectTransform), typeof(Image));
            var goRectTransform = go.GetComponent<RectTransform>();
            goRectTransform.SetParent(parent, false);
            goRectTransform.sizeDelta = size;
            goRectTransform.anchoredPosition = pos;

            var image = go.GetComponent<Image>();
            image.sprite = sprite;
            image.color = color;

            return goRectTransform;
        }

        public static Text DrawTextUI(string textString, Vector2 anchoredPosition, int fontSize, Font font) => 
            DrawTextUI(textString, GetCanvas().transform, anchoredPosition, fontSize, font);

        public static Text DrawTextUI(string textString, Transform parent, Vector2 anchoredPosition, int fontSize,
            Font font)
        {
            var textGo = new GameObject("Text", typeof(RectTransform), typeof(Text));
            textGo.transform.SetParent(parent, false);
            var textGoTrans = textGo.transform;
            textGoTrans.SetParent(parent, false);
            textGoTrans.localPosition = Vector3.zero;
            textGoTrans.localScale = Vector3.one;

            var textGoRectTransform = textGo.GetComponent<RectTransform>();
            textGoRectTransform.sizeDelta = new Vector2(0, 0);
            textGoRectTransform.anchoredPosition = anchoredPosition;

            var text = textGo.GetComponent<Text>();
            text.text = textString;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.alignment = TextAnchor.MiddleLeft;
            if (font == null)
                font = GetDefaultFont();
            text.font = font;
            text.fontSize = fontSize;

            return text;
        }

        // Parse a float, return default if failed
        public static float ParseFloat(string value, float @default) =>
            !float.TryParse(value, out var f) ? @default : f;

        // Parse a int, return default if failed
        public static int ParseInt(string value, int @default = 0) =>
            (int)ParseFloat(value, @default);

        // Get Mouse Position in World with Z = 0f
        public static Vector3 GetMouseWorldPosition() => GetMouseWorldPosition(Input.mousePosition);

        public static Vector3 GetMouseWorldPosition(Vector3 position)
        {
            var vec = GetMouseWorldPositionWithZ(position, Camera);
            vec.z = 0f;
            return vec;
        }

        public static Vector3 GetMouseWorldPositionWithZ() =>
            GetMouseWorldPositionWithZ(Input.mousePosition, Camera);

        public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) =>
            GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);

        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) =>
            worldCamera.ScreenToWorldPoint(screenPosition);
        
        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition) =>
            Camera.ScreenToWorldPoint(screenPosition);

        // Is Mouse over a UI Element? Used for ignoring World clicks through UI
        public static bool IsPointerOverUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return true;

            var pe = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            var hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pe, hits);
            return hits.Count > 0;
        }

        // Returns 00-FF, value 0->255
        public static string Dec_to_Hex(int value) => value.ToString("X2");

        // Returns 0-255
        public static int Hex_to_Dec(string hex) => Convert.ToInt32(hex, 16);

        // Returns a hex string based on a number between 0->1
        public static string Dec01_to_Hex(float value) => Dec_to_Hex((int)Mathf.Round(value * 255f));

        // Returns a float between 0->1
        public static float Hex_to_Dec01(string hex) => Hex_to_Dec(hex) / 255f;

        // Get Hex Color FF00FF
        public static string GetStringFromColor(Color color)
        {
            var red = Dec01_to_Hex(color.r);
            var green = Dec01_to_Hex(color.g);
            var blue = Dec01_to_Hex(color.b);
            return red + green + blue;
        }

        // Get Hex Color FF00FFAA
        public static string GetStringFromColorWithAlpha(Color color)
        {
            var alpha = Dec01_to_Hex(color.a);
            return GetStringFromColor(color) + alpha;
        }

        // Sets out values to Hex String 'FF'
        public static void GetStringFromColor(Color color, out string red, out string green, out string blue,
            out string alpha)
        {
            red = Dec01_to_Hex(color.r);
            green = Dec01_to_Hex(color.g);
            blue = Dec01_to_Hex(color.b);
            alpha = Dec01_to_Hex(color.a);
        }

        // Get Hex Color FF00FF
        public static string GetStringFromColor(float r, float g, float b)
        {
            var red = Dec01_to_Hex(r);
            var green = Dec01_to_Hex(g);
            var blue = Dec01_to_Hex(b);
            return red + green + blue;
        }

        // Get Hex Color FF00FFAA
        public static string GetStringFromColor(float r, float g, float b, float a)
        {
            var alpha = Dec01_to_Hex(a);
            return GetStringFromColor(r, g, b) + alpha;
        }

        // Get Color from Hex string FF00FFAA
        public static Color GetColorFromString(string color)
        {
            var red = Hex_to_Dec01(color.Substring(0, 2));
            var green = Hex_to_Dec01(color.Substring(2, 2));
            var blue = Hex_to_Dec01(color.Substring(4, 2));
            var alpha = 1f;
            if (color.Length >= 8)
            {
                // Color string contains alpha
                alpha = Hex_to_Dec01(color.Substring(6, 2));
            }

            return new Color(red, green, blue, alpha);
        }

        public static Vector3 GetNormalizedRandomVector3() =>
            GetRandomVector3().normalized;

        public static Vector3 GetRandomVector3() =>
            new(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));

        public static Vector3 GetVectorFromAngle(int angle)
        {
            // angle = 0 -> 360
            var angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }

        public static float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            var n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }

        public static int GetAngleFromVector(Vector3 dir)
        {
            dir = dir.normalized;
            var n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) 
                n += 360;
            var angle = Mathf.RoundToInt(n);

            return angle;
        }

        public static int GetAngleFromVector180(Vector3 dir)
        {
            dir = dir.normalized;
            var n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var angle = Mathf.RoundToInt(n);

            return angle;
        }

        public static Vector3 ApplyRotationToVector(Vector3 vec, Vector3 vecRotation) =>
            ApplyRotationToVector(vec, GetAngleFromVectorFloat(vecRotation));

        public static Vector3 ApplyRotationToVector(Vector3 vec, float angle) =>
            Quaternion.Euler(0, 0, angle) * vec;

        public static FunctionUpdater CreateMouseDraggingAction(Action<Vector3> onMouseDragging) =>
            CreateMouseDraggingAction(0, onMouseDragging);

        public static FunctionUpdater CreateMouseDraggingAction(int mouseButton, Action<Vector3> onMouseDragging)
        {
            var dragging = false;
            return FunctionUpdater.Create(() =>
            {
                if (Input.GetMouseButtonDown(mouseButton))
                {
                    dragging = true;
                }

                if (Input.GetMouseButtonUp(mouseButton))
                {
                    dragging = false;
                }

                if (dragging)
                {
                    onMouseDragging(GetMouseWorldPosition());
                }

                return false;
            });
        }

        public static FunctionUpdater CreateMouseClickFromToAction(Action<Vector3, Vector3> onMouseClickFromTo,
            Action<Vector3, Vector3> onWaitingForToPosition) =>
            CreateMouseClickFromToAction(0, 1, onMouseClickFromTo, onWaitingForToPosition);

        public static FunctionUpdater CreateMouseClickFromToAction(int mouseButton, int cancelMouseButton,
            Action<Vector3, Vector3> onMouseClickFromTo, Action<Vector3, Vector3> onWaitingForToPosition)
        {
            var state = 0;
            var from = Vector3.zero;
            return FunctionUpdater.Create(() =>
            {
                if (state == 1)
                {
                    onWaitingForToPosition?.Invoke(from, GetMouseWorldPosition());
                }

                if (state == 1 && Input.GetMouseButtonDown(cancelMouseButton))
                {
                    // Cancel
                    state = 0;
                }

                if (Input.GetMouseButtonDown(mouseButton) && !IsPointerOverUI())
                {
                    if (state == 0)
                    {
                        state = 1;
                        from = GetMouseWorldPosition();
                    }
                    else
                    {
                        state = 0;
                        onMouseClickFromTo(from, GetMouseWorldPosition());
                    }
                }

                return false;
            });
        }

        public static FunctionUpdater CreateMouseClickAction(Action<Vector3> onMouseClick, int mouseButton = 0)
        {
            return FunctionUpdater.Create(() =>
            {
                if (Input.GetMouseButtonDown(mouseButton))
                {
                    onMouseClick(GetWorldPositionFromUI());
                }

                return false;
            });
        }

        public static FunctionUpdater CreateKeyCodeAction(KeyCode keyCode, Action onKeyDown)
        {
            return FunctionUpdater.Create(() =>
            {
                if (Input.GetKeyDown(keyCode))
                {
                    onKeyDown();
                }

                return false;
            });
        }

        // Get UI Position from World Position
        public static Vector2 GetWorldUIPosition(Vector3 worldPosition, Transform parent, Camera uiCamera,
            Camera worldCamera)
        {
            var screenPosition = worldCamera.WorldToScreenPoint(worldPosition);
            var uiCameraWorldPosition = uiCamera.ScreenToWorldPoint(screenPosition);
            var localPos = parent.InverseTransformPoint(uiCameraWorldPosition);
            return new Vector2(localPos.x, localPos.y);
        }

        public static Vector3 GetWorldPositionFromUIWithZeroZ()
        {
            var vec = GetWorldPositionFromUI(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }

        // Get World Position from UI Position
        public static Vector3 GetWorldPositionFromUI() =>
            GetWorldPositionFromUI(Input.mousePosition, Camera);

        public static Vector3 GetWorldPositionFromUI(Camera camera) =>
            GetWorldPositionFromUI(Input.mousePosition, camera);

        public static Vector3 GetWorldPositionFromUI(Vector3 screenPosition, Camera camera) =>
            camera.ScreenToWorldPoint(screenPosition);

        public static Vector3 GetWorldPositionFromUI_Perspective() =>
            GetWorldPositionFromUI_Perspective(Input.mousePosition, Camera.main);

        public static Vector3 GetWorldPositionFromUI_Perspective(Camera worldCamera) =>
            GetWorldPositionFromUI_Perspective(Input.mousePosition, worldCamera);

        public static Vector3 GetWorldPositionFromUI_Perspective(Vector3 screenPosition, Camera worldCamera)
        {
            var ray = worldCamera.ScreenPointToRay(screenPosition);
            var plane = new Plane(Vector3.forward, new Vector3(0, 0, 0f));
            plane.Raycast(ray, out var distance);
            return ray.GetPoint(distance);
        }

        public static void ShakeCamera(float intensity, float timer)
        {
            var lastCameraMovement = Vector3.zero;
            FunctionUpdater.Create(delegate()
            {
                timer -= Time.unscaledDeltaTime;
                var randomMovement =
                    new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized *
                    intensity;
                var cameraTransform = Camera.transform;
                cameraTransform.position = cameraTransform.position - lastCameraMovement + randomMovement;
                lastCameraMovement = randomMovement;
                return timer <= 0f;
            }, "CAMERA_SHAKE");
        }
    }
}