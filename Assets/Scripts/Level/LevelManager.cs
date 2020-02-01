using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityTemplateProjects.GameConfigs;
using Utils;

namespace UnityTemplateProjects.Level
{
    public class LevelManager : BaseSingleton<LevelManager>
    {
        [SerializeField] private Map map;

        private LevelEconomy _currentLevelEconomyData;
        private int _currentScore;
        private float _elapsedTime;
        private int _currentLevel;

        public int CurrentScore
        {
            get => _currentScore;
            set
            {
                _currentScore = value;
                EventManager.GetInstance().Publish(GGJEventType.ScoreUpdated);
            }
        }

        public int CurrentLevel => _currentLevel;

        public float RemainingTime => Mathf.Max(0, _currentLevelEconomyData.duration - _elapsedTime);

        private void Start()
        {
            _currentLevel = 1;
            _currentLevelEconomyData = LevelEconomyProvider.GetEconomyForLevel(_currentLevel);
            _elapsedTime = 0;
            SceneManager.LoadSceneAsync("OverlayUi", LoadSceneMode.Additive);
            Invoke(nameof(GenerateMap), 2);
            Invoke(nameof(ActivateLanes), 3);

            StartCoroutine(RunLevel());
        }

        private IEnumerator RunLevel()
        {
            var secondsPassed = 0;
            while (_currentLevelEconomyData.duration > _elapsedTime)
            {
                yield return null;
                _elapsedTime += Time.deltaTime;
                var currentElapsedSeconds = (int)Mathf.Floor(_elapsedTime);

                if (currentElapsedSeconds > secondsPassed)
                {
                    secondsPassed = currentElapsedSeconds;
                    EventManager.GetInstance().Publish(GGJEventType.TimeUpdated);
                }
            }
            
            EndLevel();
        }

        private void EndLevel()
        {
            // Do end level logic
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