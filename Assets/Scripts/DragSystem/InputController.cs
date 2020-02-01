using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using System.Reflection;
#endif
using UnityEngine;
using Utils;

namespace DragSystem
{
    public class InputController : BaseSingleton<InputController>
    {
        private HashSet<Collider2D> _hoveredColliders = new HashSet<Collider2D>();

        public void Update()
        {
            DetectPointerUp();

            RaycastHit2D[] raycastHits;
            if (Camera.main != null)
            {
                var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                raycastHits = Physics2D.RaycastAll(ray, Vector2.zero);

                if (raycastHits.Length == 0)
                {
                    return;
                }
            }
            else
            {
                raycastHits = new RaycastHit2D[100];
            }

            var touches = InputHelper.GetTouches();

            if (touches.Count == 0)
            {
                _hoveredColliders?.Clear();
                return;
            }

            var isDown = touches[0].phase == TouchPhase.Began;
            var isDrag = touches[0].phase == TouchPhase.Moved || touches[0].phase == TouchPhase.Stationary;
            var isUp = touches[0].phase == TouchPhase.Ended;

            var currentColliders = new HashSet<Collider2D>();
            foreach (var raycastHit in raycastHits)
            {
                if (isDown)
                {
                    var ggDown = raycastHit.collider.GetComponent<IGgPointerDown>();
                    ggDown?.OnGgPointerDown();
                }

                if (isDrag)
                {
                    var ggDrag = raycastHit.collider.GetComponent<IGgDrag>();
                    ggDrag?.OnGgDrag();
                }

                if (isUp)
                {
                    var ggUp = raycastHit.collider.GetComponent<IGgPointerUp>();
                    ggUp?.OnGgPointerUp();
                }

                currentColliders.Add(raycastHit.collider);
                
                if (!_hoveredColliders.Add(raycastHit.collider))
                {
                    continue;
                }
                
                var ggEnter = raycastHit.collider.GetComponent<IGgPointerEnter>();
                ggEnter?.OnGgPointerEnter();
                
            }

            var exitColliders =
                _hoveredColliders.Where(c => !currentColliders.Contains(c)).ToArray();

            foreach (var exitCollider in exitColliders)
            {
                var ggExit = exitCollider.GetComponent<IGgPointerExit>();
                ggExit?.OnGgPointerExit();
            }

            _hoveredColliders.ExceptWith(exitColliders);
        }

        private static void DetectPointerUp()
        {
            if ((Input.touches.Length <= 0 || Input.touches[0].phase != TouchPhase.Ended)
                && !Input.GetMouseButtonUp(0))
            {
                return;
            }

            Debug.Log("====InputController===Up");
            EventManager.GetInstance().Publish(GGJEventType.InputTouchUp);
        }
    }


    public class InputHelper : MonoBehaviour
    {
        private static TouchCreator lastFakeTouch;

        public static List<Touch> GetTouches()
        {
            List<Touch> touches = new List<Touch>();
            touches.AddRange(Input.touches);
#if UNITY_EDITOR
            if (lastFakeTouch == null) lastFakeTouch = new TouchCreator();
            if (Input.GetMouseButtonDown(0))
            {
                lastFakeTouch.phase = TouchPhase.Began;
                lastFakeTouch.deltaPosition = new Vector2(0, 0);
                lastFakeTouch.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                lastFakeTouch.fingerId = 0;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                lastFakeTouch.phase = TouchPhase.Ended;
                Vector2 newPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                lastFakeTouch.deltaPosition = newPosition - lastFakeTouch.position;
                lastFakeTouch.position = newPosition;
                lastFakeTouch.fingerId = 0;
            }
            else if (Input.GetMouseButton(0))
            {
                lastFakeTouch.phase = TouchPhase.Moved;
                Vector2 newPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                lastFakeTouch.deltaPosition = newPosition - lastFakeTouch.position;
                lastFakeTouch.position = newPosition;
                lastFakeTouch.fingerId = 0;
            }
            else
            {
                lastFakeTouch = null;
            }

            if (lastFakeTouch != null) touches.Add(lastFakeTouch.Create());
#endif


            return touches;
        }

    }

#if UNITY_EDITOR
    public class TouchCreator
    {
        static BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
        static Dictionary<string, FieldInfo> fields;

        object touch;

        public float deltaTime
        {
            get { return ((Touch) touch).deltaTime; }
            set { fields["m_TimeDelta"].SetValue(touch, value); }
        }

        public int tapCount
        {
            get { return ((Touch) touch).tapCount; }
            set { fields["m_TapCount"].SetValue(touch, value); }
        }

        public TouchPhase phase
        {
            get { return ((Touch) touch).phase; }
            set { fields["m_Phase"].SetValue(touch, value); }
        }

        public Vector2 deltaPosition
        {
            get { return ((Touch) touch).deltaPosition; }
            set { fields["m_PositionDelta"].SetValue(touch, value); }
        }

        public int fingerId
        {
            get { return ((Touch) touch).fingerId; }
            set { fields["m_FingerId"].SetValue(touch, value); }
        }

        public Vector2 position
        {
            get { return ((Touch) touch).position; }
            set { fields["m_Position"].SetValue(touch, value); }
        }

        public Vector2 rawPosition
        {
            get { return ((Touch) touch).rawPosition; }
            set { fields["m_RawPosition"].SetValue(touch, value); }
        }

        public Touch Create()
        {
            return (Touch) touch;
        }

        public TouchCreator()
        {
            touch = new Touch();
        }

        static TouchCreator()
        {
            fields = new Dictionary<string, FieldInfo>();
            foreach (var f in typeof(Touch).GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                fields.Add(f.Name, f);
            }
        }
    }
#endif
}