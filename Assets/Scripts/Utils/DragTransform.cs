using UnityEngine;

namespace Utils
{
    public class DragTransform : MonoBehaviour
    {
        private bool dragging;
        private float distance;
        
        /*void OnMouseEnter()
        {
            renderer.material.color = mouseOverColor;
        }
 
        void OnMouseExit()
        {
            renderer.material.color = originalColor;
        }*/

        private void OnMouseDown()
        {
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
        }

        private void OnMouseUp()
        {
            dragging = false;
        }
        
        private void Update()
        {
            if (dragging)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var rayPoint = ray.GetPoint(distance);
                transform.position = rayPoint;
            }
        }
    }
}