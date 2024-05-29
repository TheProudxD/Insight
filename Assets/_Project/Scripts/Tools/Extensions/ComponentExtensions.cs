using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{
    public static class ComponentExtensions
    {
        public static void Activate(this GameObject component)
        {
            if (component) component.gameObject.SetActive(true);
        }

        public static void Deactivate(this GameObject component)
        {
            if (component) component.gameObject.SetActive(false);
        }

        public static void Activate(this Component component)
        {
            if (component) component.gameObject.SetActive(true);
        }

        public static void Deactivate(this Component component)
        {
            if (component) component.gameObject.SetActive(false);
        }

        public static T GetOrAddComponent<T>(this Component child) where T : Component =>
            child.GetComponent<T>() ?? child.gameObject.AddComponent<T>();
        
        // ïîëåçåí åñëè íàäî îáíîâèòü îòðèñîâêó ìàêåòà, êîãäà íà ýëåìåíòå îäèí èç Layout-îâ
        public static void RefreshLayout(this RectTransform transform, bool hard = false)
        {
            if (hard)
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform); // Ïðèíóäèòåëüíî ïåðåñòðàèâàåì ìàêåò
            else
                LayoutRebuilder.MarkLayoutForRebuild(transform); // Ïîìå÷àåì ìàêåò äëÿ ïåðåñòðîåíèÿ
        }

        //ïîëåçåí ïðè ðàáîòå ñ DoTween, åñëè íàäî ñáðîñèòü öâåò ïåðåä Àíèìàöèåé
        public static void SetColorAlpha(this MaskableGraphic targetGraphic, float newAlpha)
        {
            var backColor = targetGraphic.color;
            backColor.a = newAlpha;
            targetGraphic.color = backColor;
        }
    }
}