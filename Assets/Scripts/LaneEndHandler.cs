using UnityEngine;
using Utils;

namespace UnityTemplateProjects
{
    [RequireComponent(typeof(Collider2D))]
    public class LaneEndHandler : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponentInParent<CarController>(out var carController))
            {
                carController.ClaimCar();
            }
        }
    }
}