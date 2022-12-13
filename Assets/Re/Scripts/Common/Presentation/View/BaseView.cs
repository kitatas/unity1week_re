using UnityEngine;

namespace Re.Common.Presentation.View
{
    public abstract class BaseView<T> : MonoBehaviour where T : new()
    {
        public abstract void Render(T value);
    }
}