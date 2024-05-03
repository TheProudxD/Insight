using System;
using System.Collections.Generic;
using Extensions;
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

        public static float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            var n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
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
                    onMouseDragging(_camera.GetMouseWorldPosition());
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
                    onWaitingForToPosition?.Invoke(from, _camera.GetMouseWorldPosition());
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
                        from = _camera.GetMouseWorldPosition();
                    }
                    else
                    {
                        state = 0;
                        onMouseClickFromTo(from, _camera.GetMouseWorldPosition());
                    }
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
    }
}