using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace UnityTemplateProjects
{
    [RequireComponent(typeof(Collider2D))]
    public class CrossWalkController : MonoBehaviour
    {
        private readonly ISet<CarController> _hitObjects = new HashSet<CarController>();
        [SerializeField] private bool _isLocked;
        
        public bool IsLocked
        {
            get => _isLocked;
            set
            {
                if (!value)
                {
                    foreach (var carController in _hitObjects)
                    {
                        carController.SetStopped(false);
                    }
                    
                    _hitObjects.Clear();
                }
                
                _isLocked = value;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isLocked && other.gameObject.CompareTag(Constants.FrontCarColliderTag) &&
                other.gameObject.TryGetComponentInParent<CarController>(out var carController))
            {
                carController.SetStopped(true);
                _hitObjects.Add(carController);
            }
        }
    }
}