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
            await _beginDragTrigger.OnBeginDragAsync(token);

            _handleView = Instantiate(handleView, transform.parent);
            _handleView.SetUp(playerPosition);

            var pointerEventData =  await _endDragTrigger.OnEndDragAsync(token);
            return pointerEventData.GetDragDiff();
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