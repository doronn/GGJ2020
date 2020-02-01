using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityTemplateProjects;
using UnityTemplateProjects.World;
using Utils;

namespace DragSystem
{ 
    public class DraggableObject : MonoBehaviour, IGgPointerDown, IGgPointerUp
    {
        private const int DEFUALT_LAYER = 1;
        private const int HELD_LAYER = 2;
        private const float MOVE_BACK_SPEED = 50f;

        /*SerializeField]
        private SpriteRenderer[] _spriteRenderers;*/

        [SerializeField]
        private DropTarget _originalParent;
        
        private float _distanceFromCamera;
        private bool _isHolding;
        private UnityAction _onReleasedOnValidDropTarget;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
            if (_mainCamera == null)
            {
                return;
            }
            
            var distanceFromCamera = _mainCamera.transform.position - transform.position;
            _distanceFromCamera = Mathf.Abs(distanceFromCamera.z);
        }

        private static IEnumerator WaitForEndOfFrame(Action doAfterFrame)
        {
            yield return new WaitForEndOfFrame();
            doAfterFrame?.Invoke();
        }

        public void KillCar()
        {
            _isHolding = false;
            StopAllCoroutines();
            EventManager.GetInstance()
                .UnSubscribe(GGJEventType.ReleasedOnValidDropTarget, _onReleasedOnValidDropTarget);
            CarHeldController.GetInstance().currentlyHeldCarController = null;
            _originalParent.ResetIsOriginal();
            
            Destroy(gameObject);
        }

        public void OnGgPointerDown()
        {
            _isHolding = true;
            CarHeldController.GetInstance().currentlyHeldCarController = _originalParent.GetComponent<CarController>();
            /*foreach (var spriteRenderer in _spriteRenderers)
            {
                spriteRenderer .sortingOrder = HELD_LAYER;
            }*/
            
            transform.SetParent(WorldRootController.GetInstance().WorldRootTransform);

            _onReleasedOnValidDropTarget = () =>
            {
                gameObject.SetActive(false);
            };
            EventManager.GetInstance().Subscribe(GGJEventType.ReleasedOnValidDropTarget, _onReleasedOnValidDropTarget);

            StartCoroutine(FollowPointer());
        }

        public void OnGgPointerUp()
        {
            if (!_isHolding)
            {
                return;
            }
            _isHolding = false;
            
            EnumerationObject.GetInstance().StartCoroutine(WaitForEndOfFrame(() =>
            {
                CarHeldController.GetInstance().currentlyHeldCarController = null;
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
                /*foreach (var spriteRenderer in _spriteRenderers)
                {
                    spriteRenderer.sortingOrder = DEFUALT_LAYER;
                }*/
            });
        }

        private IEnumerator FollowPointer()
        {
            while (_isHolding)
            {
                var mousePosition = Input.mousePosition;
                mousePosition.z = _distanceFromCamera;
                mousePosition = _mainCamera.ScreenToWorldPoint(mousePosition);
                transform.position = mousePosition;
                yield return null;
            }
        }
    }
}