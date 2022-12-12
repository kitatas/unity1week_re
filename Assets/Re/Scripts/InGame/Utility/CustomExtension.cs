using UnityEngine;
using UnityEngine.EventSystems;

namespace Re.InGame
{
    public static class CustomExtension
    {
        public static Vector2 GetDragDiff(this PointerEventData pointerEventData)
        {
            return pointerEventData.pressPosition - pointerEventData.position;
        }

        public static void SetWorldToLocalPoint(this RectTransform rectTransform, Camera camera, Vector2 targetPoint)
        {
            var screenPoint = RectTransformUtility.WorldToScreenPoint(camera, targetPoint);
            RectTransformUtility
                .ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, camera, out var localPoint);
            rectTransform.localPosition = localPoint;
        }
    }
}