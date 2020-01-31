using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace DragSystem
{
    public class DropTarget : MonoBehaviour, IGgPointerDown, IGgPointerUp
    {
        // private UnityAction _onInputMouseUp;
        // private bool _isHover = false;
        private bool _isOriginal = false;

        private void Awake()
        {
            // _isHover = false;
            /*_onInputMouseUp = () =>
            {
                if (!_isHover || _isOriginal)
                {
                    _isOriginal = false;
                    return;
                }
                _isHover = false;

                // TODO: Check merge condition
                Debug.Log($"<color=blue>***DropTarget***</color> Trying to merge {name}");
                EventManager.GetInstance().Publish(GGJEventType.ReleasedOnValidDropTarget);
            };
            EventManager.GetInstance().Subscribe(GGJEventType.InputTouchUp, _onInputMouseUp);*/

        }

        private void OnDestroy()
        {
            /*if (_onInputMouseUp != null)
            {
                EventManager.GetInstance().UnSubscribe(GGJEventType.InputTouchUp, _onInputMouseUp);
            }

            _onInputMouseUp = null;*/
        }

        public void OnGgPointerDown()
        {
            Debug.Log($"<color=blue>***DropTarget***</color> MouseDown {name}");
            _isOriginal = true;
        }

        public void OnGgPointerEnter()
        {
            Debug.Log($"<color=blue>***DropTarget***</color> Enter {name}");
            // _isHover = true;
        }

        public void OnGgPointerExit()
        {
            Debug.Log($"<color=blue>***DropTarget***</color> Exit {name}");
            // _isHover = false;
        }

        public void OnGgPointerUp()
        {
            if (_isOriginal)
            {
                _isOriginal = false;
                return;
            }

            Debug.Log($"<color=blue>***DropTarget***</color> Trying to merge {name}");
            // TODO: Check merge condition
            EventManager.GetInstance().Publish(GGJEventType.ReleasedOnValidDropTarget);

        }
    }
}