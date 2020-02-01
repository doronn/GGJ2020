using UnityEngine;
using Utils;

namespace DragSystem
{
    public class DropTarget : MonoBehaviour, IGgPointerDown, IGgPointerUp
    {
        private bool _isHover = false;
        private bool _isOriginal = false;

        public void OnGgPointerDown()
        {
            _isOriginal = true;
        }

        public void OnGgPointerUp()
        {
            if (_isOriginal)
            {
                _isOriginal = false;
                return;
            }

            // TODO: Check merge condition
            EventManager.GetInstance().Publish(GGJEventType.ReleasedOnValidDropTarget);

        }

        public void ResetIsOriginal()
        {
            _isOriginal = false;
        }
    }
}