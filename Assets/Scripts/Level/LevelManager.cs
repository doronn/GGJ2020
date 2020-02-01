using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace UnityTemplateProjects.Level
{
    public class LevelManager : BaseSingleton<LevelManager>
    {
        [SerializeField] private Map map;
        private int _currentScore;

        public int CurrentScore
        {
            get => _currentScore;
            set
            {
                _currentScore = value;
                EventManager.GetInstance().Publish(GGJEventType.ScoreUpdated);
            }
        }

        private void Start()
        {
            SceneManager.LoadSceneAsync("OverlayUi", LoadSceneMode.Additive);
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