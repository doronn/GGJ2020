using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityTemplateProjects.World;
using Utils;

namespace DragSystem
{
    public class DraggableObject : MonoBehaviour, IGgDrag, IGgPointerDown, IGgPointerUp
    {
        private const int DEFUALT_LAYER = 1;
        private const int HELD_LAYER = 2;
        private const float MOVE_BACK_SPEED = 50f;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Transform _originalParent;


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
            
            // Debug.Log($"<color=green>!!!!!!DraggableObject!!!!!!</color> Dragging {name}");
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
            Debug.Log($"<color=green>!!!!!!DraggableObject!!!!!!</color> Mouse down {name}");
            _isHolding = true;
            _spriteRenderer.sortingOrder = HELD_LAYER;
            transform.SetParent(WorldRootController.GetInstance().WorldRootTransform);

            _onReleasedOnValidDropTarget = () =>
            {
                Debug.Log($"<color=yellow>Pop</color> {name}");
                gameObject.SetActive(false);
            };
            EventManager.GetInstance().Subscribe(GGJEventType.ReleasedOnValidDropTarget, _onReleasedOnValidDropTarget);
        }

        public void OnGgPointerUp()
        {
            _isHolding = false;
            Debug.Log($"<color=green>!!!!!!DraggableObject!!!!!!</color> Mouse Up {name}");

            EnumerationObject.GetInstance().StartCoroutine(WaitForFrame(() =>
            {
                if (_isHolding)
                {
                    return;
                }
                
                Debug.Log($"<color=green>!!!!!!DraggableObject!!!!!!</color> unsubscribe release {name}");

                EventManager.GetInstance()
                    .UnSubscribe(GGJEventType.ReleasedOnValidDropTarget, _onReleasedOnValidDropTarget);
            }));
            
            transform.SetParent(_originalParent);

            var dist = Vector3.Distance(transform.localPosition, Vector2.zero);
            transform.LeanMoveLocal(Vector2.zero, dist / MOVE_BACK_SPEED).setOnComplete(o =>
            {
                _spriteRenderer.sortingOrder = DEFUALT_LAYER;
            });
        }
    }
}