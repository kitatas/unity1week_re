using EFUK;
using UnityEngine;

namespace Re.InGame.Presentation.View
{
    public sealed class HandleView : MonoBehaviour
    {
        public void SetUp(Vector2 playerPosition)
        {
            transform
                .ConvertRectTransform()
                .SetWorldToLocalPoint(Camera.main, playerPosition);
        }

        public void Drag(Vector2 dragDiff)
        {
            var rectTransform = transform.ConvertRectTransform();

            // 矢印の向き
            var direction = Quaternion.FromToRotation(Vector3.up, dragDiff);
            transform.rotation = direction;

            // 矢印の長さ
            rectTransform.sizeDelta = rectTransform.sizeDelta.SetY(dragDiff.magnitude * 0.01f);
        }
    }
}