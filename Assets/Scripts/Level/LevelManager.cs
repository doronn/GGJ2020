using UnityEngine;

namespace UnityTemplateProjects.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Map map;

        private void Start()
        {
            Invoke(nameof(GenerateMap), 2);
            Invoke(nameof(ActivateLanes), 3);
        }
        
        public void GenerateMap()
        {
            map.Generate(3);
        }

        public void ActivateLanes()
        {
            foreach (var laneController in map.laneControllers)
            {
                laneController.isOn = true;
            }
        }
    }
}