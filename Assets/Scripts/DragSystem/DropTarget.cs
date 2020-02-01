using UnityEngine;
using UnityTemplateProjects;
using Utils;

namespace DragSystem
{
    public class DropTarget : MonoBehaviour, IGgPointerDown, IGgPointerUp
    {
        private bool _isHover = false;
        private bool _isOriginal;

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

            if (TryGetComponent<CarController>(out var carController))
            {
                var draggedCar = CarHeldController.GetInstance().currentlyHeldCarController;
                
                if (carController.CanMerge(draggedCar))
                {
                    carController.CopyPassengersFrom(draggedCar);
                    EventManager.GetInstance().Publish(GGJEventType.ReleasedOnValidDropTarget);
                }
            }
        }
        
        public void ResetIsOriginal()
        {
            _isOriginal = false;
        }
    }
}