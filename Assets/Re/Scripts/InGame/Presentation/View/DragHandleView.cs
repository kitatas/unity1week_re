using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Re.InGame.Presentation.View
{
    public sealed class DragHandleView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private HandleView handleView = default;

        private HandleView _handleView;
        private AsyncBeginDragTrigger _beginDragTrigger;
        private AsyncEndDragTrigger _endDragTrigger;

        public void Init()
        {
            _beginDragTrigger = this.GetAsyncBeginDragTrigger();
            _endDragTrigger = this.GetAsyncEndDragTrigger();
        }

        public async UniTask<Vector2> SetUpAsync(Vector2 playerPosition, CancellationToken token)
        {
            var (index, _, _) = await UniTask.WhenAny(
                _beginDragTrigger.OnBeginDragAsync(token),
                ReleasePointerAsync(token)
            );

            // PointerUpされていた場合
            if (index == 1)
            {
                return Vector2.zero;
            }

            _handleView = Instantiate(handleView, transform.parent);
            _handleView.SetUp(playerPosition);

            var pointerEventData = await _endDragTrigger.OnEndDragAsync(token);
            return pointerEventData.GetDragDiff();
        }

        private async UniTask<PointerEventData> ReleasePointerAsync(CancellationToken token)
        {
            await UniTask.WaitUntil(() => Input.GetMouseButtonUp(0), cancellationToken: token);

            return new PointerEventData(EventSystem.current);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_handleView)
            {
                _handleView.Drag(eventData.GetDragDiff());
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_handleView)
            {
                Destroy(_handleView.gameObject);
            }
        }
    }
}