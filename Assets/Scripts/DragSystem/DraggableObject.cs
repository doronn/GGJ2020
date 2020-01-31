using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityTemplateProjects.World;
using Utils;

namespace DragSystem
{
    public class DraggableObject : MonoBehaviour, IGgDrag, IGgPointerDown, IGgPointerUp, IGgPointerExit
    {
        private const int DEFUALT_LAYER = 1;
        private const int HELD_LAYER = 2;
        private const float MOVE_BACK_SPEED = 50f;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private DropTarget _originalParent;


        private float _distanceFromCamera;
        private bool _isHolding = false;
        private UnityAction _onReleasedOnValidDropTarget;

        private void Awake()
        {
            var distanceFromCamera = Camera.main.transform.position - transform.position;
            _distanceFromCamera = Mathf.Abs(distanceFromCamera.z);
        }

        private IEnumerator WaitForFrame(Action doAfterFrame)
        {
            yield return null;
            doAfterFrame?.Invoke();
        }

        public void OnGgDrag()
        {
            if (!_isHolding)
            {
                return;
            }
            
            if (Camera.main == null)
            {
                return;
            }

            var mousePosition = Input.mousePosition;
            mousePosition.z = _distanceFromCamera;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = mousePosition;
        }

        public void OnGgPointerDown()
        {
            _isHolding = true;
            _spriteRenderer.sortingOrder = HELD_LAYER;
            transform.SetParent(WorldRootController.GetInstance().WorldRootTransform);

            _onReleasedOnValidDropTarget = () =>
            {
                gameObject.SetActive(false);
            };
            EventManager.GetInstance().Subscribe(GGJEventType.ReleasedOnValidDropTarget, _onReleasedOnValidDropTarget);
            
            OnGgDrag();
        }

        public void OnGgPointerUp()
        {
            if (!_isHolding)
            {
                return;
            }
            _isHolding = false;
            
            EnumerationObject.GetInstance().StartCoroutine(WaitForFrame(() =>
            {
                _originalParent.ResetIsOriginal();
                if (_isHolding)
                {
                    return;
                }

                EventManager.GetInstance()
                    .UnSubscribe(GGJEventType.ReleasedOnValidDropTarget, _onReleasedOnValidDropTarget);
            }));
            
            transform.SetParent(_originalParent.transform);

            var dist = Vector3.Distance(transform.localPosition, Vector2.zero);
            transform.LeanMoveLocal(Vector2.zero, dist / MOVE_BACK_SPEED).setOnComplete(o =>
            {
                _spriteRenderer.sortingOrder = DEFUALT_LAYER;
            });
        }

        public void OnGgPointerExit()
        {
            OnGgPointerUp();
        }
    }
}