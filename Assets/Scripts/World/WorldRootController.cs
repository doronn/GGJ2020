using UnityEngine;
using Utils;

namespace UnityTemplateProjects.World
{
    public class WorldRootController : BaseSingleton<WorldRootController>
    {
        [SerializeField]
        private Transform _worldRootTransform;

        public Transform WorldRootTransform => _worldRootTransform;
    }
}